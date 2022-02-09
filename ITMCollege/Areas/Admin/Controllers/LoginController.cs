using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
