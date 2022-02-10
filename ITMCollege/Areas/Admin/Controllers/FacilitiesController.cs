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
    public class FacilitiesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;
        

        private readonly string uri = "http://localhost:20646/api/facilities/";
        private HttpClient httpclient = new HttpClient();

        public FacilitiesController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }
        // GET: FacilitiesController
        public ActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Facility>>(httpclient.GetStringAsync(uri).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: FacilitiesController/Details/5
        public ActionResult Details(int id)
        {
            var model = JsonConvert.DeserializeObject<Facility>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: FacilitiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FacilitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacilityId,FacilityName,IsActive,Image")] Facility facility, IFormFile file)
        {
           if(ModelState.IsValid)
            {
                string fileName = Path.GetFileName(file.FileName);
                string file_path = Path.Combine
                    (Directory.GetCurrentDirectory(), @"wwwroot/Images/Facility", fileName);
                using (var stream = new FileStream(file_path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                facility.Image = "Images/Facility/" + fileName;
                var data = httpclient.PostAsJsonAsync<Facility>(uri, facility).Result;
                if (data.IsSuccessStatusCode)
                {
                    _notyf.Success("Create Succesfully");
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(facility);
        }

        // GET: FacilitiesController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = JsonConvert.DeserializeObject<Facility>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
          
        }

        // POST: FacilitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("FacilityId,FacilityName,IsActive,Image")] Facility facility, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string file_path = Path.Combine
                        (Directory.GetCurrentDirectory(), @"wwwroot/Images/Facility", fileName);
                    using (var stream = new FileStream(file_path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    facility.Image = "Images/Facility/" + fileName;
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uri + id, facility).Result;
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uri + id, facility).Result;
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: FacilitiesController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = JsonConvert.DeserializeObject<Facility>(httpclient.GetStringAsync(uri + id).Result);
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
