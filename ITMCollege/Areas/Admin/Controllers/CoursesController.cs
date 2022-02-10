using AspNetCoreHero.ToastNotification.Abstractions;
using ITMCollege.Areas.Client.Controllers;
using ITMCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;

        private readonly string uri = "http://localhost:20646/api/courses/";
        private readonly string uri1 = "http://localhost:20646/api/fields/";
        private readonly string uri11 = "http://localhost:20646/api/fields/GetFieldsByStreamId/";
        private readonly string uri2 = "http://localhost:20646/api/streams/";
        private HttpClient httpclient = new HttpClient();

        public CoursesController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }
        // GET: CoursesController
        public ActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Course>>(httpclient.GetStringAsync(uri).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: CoursesController/Details/5
        public ActionResult Details(int id)
        {
            var model = JsonConvert.DeserializeObject<Course>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        public JsonResult getfieldsFromDatabaseByStream(int id)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Field>>(httpclient.GetStringAsync(uri11 + id).Result);
            httpclient.Dispose();
            return Json(new SelectList(model,"FieldId","FieldName"));
        }

        // GET: CoursesController/Create
        public ActionResult Create()
        {
            ViewBag.ListStream = JsonConvert.DeserializeObject<IEnumerable<ITMCollege.Models.Stream>>(httpclient.GetStringAsync(uri2).Result);
            httpclient.Dispose();
            return View();
        }

        // POST: CoursesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,Description,FieldId,StreamId,Image")] Course course, IFormFile file)
        {
            try
            {
                string fileName = Path.GetFileName(file.FileName);
                string file_path = Path.Combine
                    (Directory.GetCurrentDirectory(), @"wwwroot/Images/Course", fileName);
                using (var stream = new FileStream(file_path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                course.Image = "Images/Course/" + fileName;
                var data = httpclient.PostAsJsonAsync<Course>(uri, course).Result;
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

        // GET: CoursesController/Edit/5
        public ActionResult Edit(int id)
        {
           
            ViewBag.ListStream = JsonConvert.DeserializeObject<IEnumerable<ITMCollege.Models.Stream>>(httpclient.GetStringAsync(uri2).Result);
            var model = JsonConvert.DeserializeObject<Course>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // POST: CoursesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseName,Description,FieldId,StreamId,Image")] Course course, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string file_path = Path.Combine
                        (Directory.GetCurrentDirectory(), @"wwwroot/Images/Course", fileName);
                    using (var stream = new FileStream(file_path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    course.Image = "Images/Course/" + fileName;
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uri + id, course).Result;
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uri + id, course).Result;
                    httpclient.Dispose();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CoursesController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = JsonConvert.DeserializeObject<Course>(httpclient.GetStringAsync(uri + id).Result);
            return View(data);
        }

        // POST: CoursesController/Delete/5
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
