﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models;
using System.Threading.Tasks;

namespace Shop.API.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View(ViewBag.AppUser);
        }
        
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            Login Login = new Login();
            Login.ReturnUrl = returnUrl;
            return View(Login);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                AppUser User = await _userManager.FindByEmailAsync(login.Email);
                ViewBag.AppUser = User;
                if (User != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = 
                    await _signInManager.PasswordSignInAsync(User, login.Password, false, false);                
                    if (result.Succeeded)                   
                        return Redirect(login.ReturnUrl ?? "/");
                }
                ModelState.AddModelError(nameof(login.Email), "Login failed: Invalid eamail or password ");
            }
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(User user)
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
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
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
    }
}