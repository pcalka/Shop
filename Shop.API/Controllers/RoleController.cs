using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop.API.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
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

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}