using AspNetCoreHero.ToastNotification.Abstractions;
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

namespace ITMCollege.Controllers
{
    [Area("Admin")]
    public class DepartmentsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;

        //public const string SessionKeyUsername = "_UserName";

        private readonly string uri = "http://localhost:20646/api/departments/";
        private HttpClient httpclient = new HttpClient();

        public DepartmentsController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }
        // GET: DepartmentsController
        public ActionResult Index(int pg = 1)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var model = JsonConvert.DeserializeObject<IEnumerable<Department>>(httpclient.GetStringAsync(uri).Result);
            httpclient.Dispose();
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int rescCount = model.Count();
            var pager = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = model.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
          
            return View(data);
            //return View(model);
        }

        // GET: DepartmentsController/Details/5
        public ActionResult Details(int id)
        {
            var model = JsonConvert.DeserializeObject<Department>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: DepartmentsController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        // POST: DepartmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepId,DepName,Description,Image")]Department department,IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string file_path = Path.Combine
                        (Directory.GetCurrentDirectory(), @"wwwroot/Images/Department", fileName);
                    using (var stream = new FileStream(file_path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    department.Image = "Images/Department/" + fileName;
                }
                else
                {
                    _notyf.Warning("Image file invalid");
                    return RedirectToAction(nameof(Create));
                }
                var data = httpclient.PostAsJsonAsync<Department>(uri, department).Result;
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

        // GET: DepartmentsController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = JsonConvert.DeserializeObject<Department>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // POST: DepartmentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepId,DepName,Description,Image")] Department department, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string file_path = Path.Combine
                        (Directory.GetCurrentDirectory(), @"wwwroot/Images/Department", fileName);
                    using (var stream = new FileStream(file_path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    department.Image = "Images/Department/" + fileName;
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uri + id, department).Result;
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uri + id, department).Result;
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentsController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = JsonConvert.DeserializeObject<Department>(httpclient.GetStringAsync(uri + id).Result);
            return View(data);
        }

        // POST: DepartmentsController/Delete/5
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
