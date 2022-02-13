using AspNetCoreHero.ToastNotification.Abstractions;
using ITMCollege.Areas.Client.Controllers;
using ITMCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FacultiesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;

        public const string SessionKeyUsername = "_UserName";

        private readonly string uri = "http://localhost:20646/api/faculties/";
        private readonly string uri2 = "http://localhost:20646/api/departments/";
        private HttpClient httpclient = new HttpClient();

        public FacultiesController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }
        // GET: FacultiesController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var model = JsonConvert.DeserializeObject<IEnumerable<Faculty>>(httpclient.GetStringAsync(uri).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: FacultiesController/Details/5
        public ActionResult Details(int id)
        {
            var model = JsonConvert.DeserializeObject<Faculty>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: FacultiesController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ListDep = JsonConvert.DeserializeObject<IEnumerable<Department>>(httpclient.GetStringAsync(uri2).Result);
            httpclient.Dispose();
            return View();
        }

        // POST: FacultiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacultyId,FalcultyName,Dob,Degree,DepId,Image")] Faculty faculty, IFormFile file)
        {
            try
            {
                string fileName = Path.GetFileName(file.FileName);
                string file_path = Path.Combine
                    (Directory.GetCurrentDirectory(), @"wwwroot/Images/Faculty", fileName);
                using (var stream = new FileStream(file_path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                faculty.Image = "Images/Faculty/" + fileName;
                var data = httpclient.PostAsJsonAsync<Faculty>(uri, faculty).Result;
                if (data.IsSuccessStatusCode)
                {
                    _notyf.Success("Create Succesfully");
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FacultiesController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ListDep = JsonConvert.DeserializeObject<IEnumerable<Department>>(httpclient.GetStringAsync(uri2).Result);
            var model = JsonConvert.DeserializeObject<Faculty>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // POST: FacultiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacultyId,FalcultyName,Dob,Degree,DepId,Image")] Faculty faculty, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string file_path = Path.Combine
                        (Directory.GetCurrentDirectory(), @"wwwroot/Images/Faculty", fileName);
                    using (var stream = new FileStream(file_path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    faculty.Image = "Images/Faculty/" + fileName;
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uri + id, faculty).Result;
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uri + id, faculty).Result;
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: FacultiesController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = JsonConvert.DeserializeObject<Faculty>(httpclient.GetStringAsync(uri + id).Result);
            return View(data);
        }

        // POST: FacultiesController/Delete/5
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
    }
}
