using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models;
using System.Threading.Tasks;

namespace Shop.API.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IPasswordHasher<AppUser> _passwordHasher;

        public AdminController(UserManager<AppUser> userManager,
            IPasswordHasher<AppUser> passwordHash)
        {
            _userManager = userManager;
            _passwordHasher = passwordHash;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email
                };
                IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded) return RedirectToAction("Index");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(" ", error.Description);
                    }
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Upadate(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null) 
            {
                return View(user);
            }
            return NotFound();
        }

        public async Task<IActionResult> Update(string id, string email, string password)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else ModelState.AddModelError(" ", "Email can't be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = _passwordHasher.HashPassword(user, password);
                else ModelState.AddModelError(" ", "Password can't be empty");
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                }
                else Errors(result);
            }
            else ModelState.AddModelError(" ", "User not found");
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else Errors(result);
            }
            else
            ModelState.AddModelError(" ", "User not found");
            return View("Index", _userManager.Users);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError(" ", error.Description);
        }
    }
}