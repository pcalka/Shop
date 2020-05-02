using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models;

namespace Shop.API.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<User> _userManager { get; set; }

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}