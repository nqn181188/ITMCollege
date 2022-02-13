using AspNetCoreHero.ToastNotification.Abstractions;
using ITMCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ITMCollege.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;
        public const string SessionKeyUsername = "_UserName";

        private readonly string uridep = "http://localhost:20646/api/departments/";
        private readonly string urifacul = "http://localhost:20646/api/faculties/";
        private readonly string urifaci = "http://localhost:20646/api/facilities/";
        private readonly string urifeed = "http://localhost:20646/api/feedbacks/";
        private readonly string uriacc = "http://localhost:20646/api/accounts/";
        private HttpClient httpclient = new HttpClient();

        public HomeController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyUsername)))
            {
                return RedirectToAction("Login");
            }
            ViewBag.ListDep = JsonConvert.DeserializeObject<IEnumerable<Department>>(httpclient.GetStringAsync(uridep).Result);
            ViewBag.ListFacul = JsonConvert.DeserializeObject<IEnumerable<Faculty>>(httpclient.GetStringAsync(urifacul).Result);
            ViewBag.ListFacil = JsonConvert.DeserializeObject<IEnumerable<Facility>>(httpclient.GetStringAsync(urifaci).Result);
            ViewBag.ListFeed = JsonConvert.DeserializeObject<IEnumerable<Feedback>>(httpclient.GetStringAsync(urifeed).Result);
            ViewBag.ListAcc = JsonConvert.DeserializeObject<IEnumerable<Feedback>>(httpclient.GetStringAsync(uriacc).Result);
            _notyf.Success("Success Notification");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            var check = httpclient.GetStringAsync(uriacc + userName + "/" + password).Result;
            if (check == "true")
            {
                _notyf.Success("Login Succesfully");
                if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyUsername)))
                {
                    HttpContext.Session.SetString(SessionKeyUsername, userName);
                }
                return RedirectToAction("Index");
            }
            else
            {
                //_notyf.Warning("Invalid User ID or Password.");
                ViewData["LoginMess"] = "Invalid User ID or Password.";
                return View("Login");
            }
        }
       
    }
}
