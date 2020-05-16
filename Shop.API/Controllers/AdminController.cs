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
        private IPasswordValidator<AppUser> _passwordValidator;
        private IUserValidator<AppUser> _emailValidator;

        public AdminController(UserManager<AppUser> userManager,
            IPasswordHasher<AppUser> passwordHash, IPasswordValidator<AppUser> passwordValidator, IUserValidator<AppUser> emailValidator)
        {
            _userManager = userManager;
            _passwordHasher = passwordHash;
            _passwordValidator = passwordValidator;
            _emailValidator = emailValidator;
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser NewUser = new AppUser();
                if (!string.IsNullOrEmpty(user.Name))
                {
                    NewUser.UserName = user.Name;
                }
                if (!string.IsNullOrEmpty(user.Email))
                {
                    NewUser.Email = user.Email;
                }
               
                IdentityResult Result = await _userManager.CreateAsync(NewUser, user.Password);
                if (Result.Succeeded) return RedirectToAction("Index");
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(" ", error.Description);
                    }
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            AppUser User = await _userManager.FindByIdAsync(id);
            if (User != null)
            {
                IdentityResult Result = await _userManager.DeleteAsync(User);
                if (Result.Succeeded)
                    return RedirectToAction("Index");
                else Errors(Result);
            }
            else
                ModelState.AddModelError(" ", "User not found");
            return View("Index", _userManager.Users);
        }
       
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        public async Task<IActionResult> Update(string id)
        {
            AppUser User = await _userManager.FindByIdAsync(id);
            if (User != null) 
            {
                return View(User);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            AppUser User = await _userManager.FindByIdAsync(id);
            if (User != null)
            {
                IdentityResult ValidEmial = null;
                if (!string.IsNullOrEmpty(email))
                {
                    ValidEmial = await _emailValidator.ValidateAsync(_userManager, User);
                    if (ValidEmial.Succeeded)
                        User.Email = email;
                    else Errors(ValidEmial);
                }
                else ModelState.AddModelError(" ", "Email can't be empty");
                
                IdentityResult ValidPassword = null;
                if (!string.IsNullOrEmpty(password))
                {
                    ValidPassword = await _passwordValidator.ValidateAsync(_userManager, User, password);
                    if (ValidPassword.Succeeded)
                        User.PasswordHash = _passwordHasher.HashPassword(User, password);
                    else Errors(ValidPassword);
                }
                else ModelState.AddModelError(" ", "Password can't be empty");
                
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult Result = await _userManager.UpdateAsync(User);
                    if (Result.Succeeded)
                        return RedirectToAction("Index");
                    else Errors(Result);
                }               
            }
            else ModelState.AddModelError(" ", "User not found");
            return View(User);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError(" ", error.Description);
        }
    }
}