using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models;

namespace Shop.API.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<AppUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public ViewResult Index() => View(_roleManager.Roles);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult Result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (Result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(Result);
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole Role = await _roleManager.FindByIdAsync(id);
            if (Role != null)
            {
                IdentityResult Result = await _roleManager.DeleteAsync(Role);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else Errors(Result);
            }
            else ModelState.AddModelError("", "Role not found");
            return View("Index", _roleManager.Roles);
        }

        public async Task<IActionResult> Update(string id)
        {
            IdentityRole Role = await _roleManager.FindByIdAsync(id);
            if (Role != null)
            {
                List<AppUser> Members = new List<AppUser>();
                List<AppUser> NonMembers = new List<AppUser>();
                foreach (AppUser user in _userManager.Users)
                {
                    List<AppUser> List = await _userManager.IsInRoleAsync(user, Role.Name)
                        ? Members : NonMembers;
                    List.Add(user);
                }
                return View(new RoleEdit 
                {
                    Role = Role,
                    Members = Members,
                    NonMembers = NonMembers
                });
            }
            else ModelState.AddModelError("","Role not found");
            return View("Index", _roleManager.Roles);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification role)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in role.AddIds ?? new string[] { })  
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, role.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in role.DeleteIds ?? new string[] { })
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    if(user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }               
            }
            if (ModelState.IsValid)
                return RedirectToAction("Index");
            else return await Update(role.RoleId);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}