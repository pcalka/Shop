using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.API.Controllers
{
    [Authorize]
    public class ClaimsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ClaimsController(UserManager<AppUser> userManager)
        {      
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(User?.Claims);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(string claimType, string claimValue)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
            IdentityResult result = await _userManager.AddClaimAsync(user, claim);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else Errors(result);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string claimValues)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            string[] claimValuesArray = claimValues.Split(";");
            string claimType = claimValuesArray[0], claimValue = claimValuesArray[1], 
                claimIssuer = claimValuesArray[2];
            Claim claim = User.Claims.Where(c => c.Type == claimType && c.Value == claimValue
            && c.Issuer == claimIssuer).FirstOrDefault();
            IdentityResult result = await _userManager.RemoveClaimAsync(user, claim);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
            return View("Index");
        }

        void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
