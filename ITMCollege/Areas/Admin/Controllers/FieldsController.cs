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
    public class FieldsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;

        private readonly string uri = "http://localhost:20646/api/fields/";
        private HttpClient httpclient = new HttpClient();

        public FieldsController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }
        // GET: FieldsController
        public ActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Field>>(httpclient.GetStringAsync(uri).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: FieldsController/Details/5
        public ActionResult Details(int id)
        {
            var model = JsonConvert.DeserializeObject<Field>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: FieldsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FieldsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FieldId,FieldName,StreamId")] Field field)
        {
            try
            {
               
                var data = httpclient.PostAsJsonAsync<Field>(uri, field).Result;
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

        // GET: FieldsController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = JsonConvert.DeserializeObject<Field>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // POST: FieldsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FieldId,FieldName,StreamId")] Field field)
        {
            try
            {
                if (field != null)
                {
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uri + id, field).Result;
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

        // GET: FieldsController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = JsonConvert.DeserializeObject<Field>(httpclient.GetStringAsync(uri + id).Result);
            return View(data);
        }

        // POST: FieldsController/Delete/5
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
