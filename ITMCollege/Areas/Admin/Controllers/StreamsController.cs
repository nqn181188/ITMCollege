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
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StreamsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;

        private readonly string uri = "http://localhost:20646/api/fields/";
        private HttpClient httpclient = new HttpClient();

        public StreamsController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }
        // GET: StreamsController
        public ActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Stream>>(httpclient.GetStringAsync(uri).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: StreamsController/Details/5
        public ActionResult Details(int id)
        {
            var model = JsonConvert.DeserializeObject<Stream>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: StreamsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StreamsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StreamId,StreamName")] Stream stream)
        {
            try
            {

                var data = httpclient.PostAsJsonAsync<Stream>(uri, stream).Result;
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

        // GET: StreamsController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = JsonConvert.DeserializeObject<Stream>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // POST: StreamsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StreamId,StreamName")] Stream stream)
        {
            try
            {
                if (stream != null)
                {
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uri + id, stream).Result;
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

        // GET: StreamsController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = JsonConvert.DeserializeObject<Stream>(httpclient.GetStringAsync(uri + id).Result);
            return View(data);
        }

        // POST: StreamsController/Delete/5
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
