using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shop.API.Controllers
{
    //[Authorize]
    public class ClaimsController : Controller
    {
        public IActionResult Index()
        {
            return View(User?.Claims);
        }
    }
}
