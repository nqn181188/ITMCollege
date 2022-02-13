using AspNetCoreHero.ToastNotification.Abstractions;
using ITMCollege.Areas.Client.Controllers;
using ITMCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;

        private readonly string uri = "http://localhost:20646/api/accounts/";
        private HttpClient httpclient = new HttpClient();

        public AccountsController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }
        // GET: AccountsController
        public ActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpclient.GetStringAsync(uri).Result);
            httpclient.Dispose();
            return View(model);
           
        }

        // GET: AccountsController/Details/5
        public ActionResult Details(int id)
        {
            var model = JsonConvert.DeserializeObject<Account>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        

        // GET: AccountsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountsController/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Account account  )
        {
            try
            {
                var data = httpclient.PostAsJsonAsync<Account>(uri,account).Result;
                if (data.IsSuccessStatusCode)
                {
                    _notyf.Success("Create Succesfully");
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Warning("Create fail!! UserName already exists!");
                  return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }


        }

        // GET: AccountsController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = JsonConvert.DeserializeObject<Account>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // POST: AccountsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Account account)
        {
            try
            {
                if (account != null)
                {
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uri + id, account).Result;
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Success("Edit fail");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountsController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = JsonConvert.DeserializeObject<Account>(httpclient.GetStringAsync(uri + id).Result);
          
            return View(model);
        }

        // POST: AccountsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _notyf.Success("Delete Succesfully");
                var data = httpclient.DeleteAsync(uri + id).Result;
                httpclient.Dispose();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            var check = httpclient.GetStringAsync(uri + userName + "/" + password).Result;
            if (check == "true")
            {
                _notyf.Success("Login Succesfully");
                return RedirectToAction("Index");
            }
            else
            {
                //_notyf.Warning("Invalid User ID or Password.");
                ViewData["LoginMess"] = "Invalid User ID or Password.";
                return View("Login");
            }
        }
        [HttpGet]
        public ActionResult<bool> CheckUserNameAvailability(string username)
        {
            var checkUsername = JsonConvert.DeserializeObject<Account>(httpclient.GetStringAsync(uri + username).Result);
            httpclient.Dispose();
            if (checkUsername == null)
            {
                return false;
            }
            else 
                return true;
        }
    }
}
