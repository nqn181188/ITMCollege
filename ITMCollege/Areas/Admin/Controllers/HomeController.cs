using AspNetCoreHero.ToastNotification.Abstractions;
using ITMCollege.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITMCollege.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;
        //public const string SessionKeyUsername = "_Username";
        //public const string SessionKeyRole = "_Role";

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

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("Login");
            }
            ViewBag.ListDep = JsonConvert.DeserializeObject<IEnumerable<Department>>(httpclient.GetStringAsync(uridep).Result);
            ViewBag.ListFacul = JsonConvert.DeserializeObject<IEnumerable<Faculty>>(httpclient.GetStringAsync(urifacul).Result);
            ViewBag.ListFacil = JsonConvert.DeserializeObject<IEnumerable<Facility>>(httpclient.GetStringAsync(urifaci).Result);
            ViewBag.ListFeed = JsonConvert.DeserializeObject<IEnumerable<Feedback>>(httpclient.GetStringAsync(urifeed).Result);
            ViewBag.ListAcc = JsonConvert.DeserializeObject<IEnumerable<Feedback>>(httpclient.GetStringAsync(uriacc).Result);
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
            //string pattern = @"^[A-Za-z\d#$!@%&*?]{1,30}$";
            //Match match = Regex.Match(password, pattern);
            //if (match.Success)
            //{
                var account = JsonConvert.DeserializeObject<Account>(httpclient.GetStringAsync(uriacc+ "GetAccountByUsername/" + userName).Result);
                if (account.IsActive == true)
                {
                    var checkLogin = httpclient.GetStringAsync(uriacc + userName + "/" + password).Result;
                    if (checkLogin == "true")
                    {
                        _notyf.Success("Login Succesfully");
                        //if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyUsername)))
                        //{
                        HttpContext.Session.SetString("username", account.Username);
                        HttpContext.Session.SetString("fullname", account.Fullname);
                        HttpContext.Session.SetString("role", account.Role == 1 ? "Admin" : "User");
                        //}
                        httpclient.Dispose();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _notyf.Warning("Invalid User ID or Password.");
                        return View();
                    }
                }
                else
                {
                    _notyf.Warning("Account is not active yet.");
                    return View();
                }
                
            //}
            //else
            //{
            //    _notyf.Warning(@"The field Password must match the regular expression '^[A-Za-z\d#$!@%&*?]{1,30}$'. ");
            //    return RedirectToAction("Login");
            //}
        }
       
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            _notyf.Success("Logout Succesfully");
            return View("Login");
        }

    }
}
