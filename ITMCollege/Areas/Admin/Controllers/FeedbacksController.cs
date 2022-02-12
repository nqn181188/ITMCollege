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
    public class FeedbacksController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;

        private readonly string uri = "http://localhost:20646/api/feedbacks/";
        private HttpClient httpclient = new HttpClient();

        public FeedbacksController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }
        // GET: FeedBacksController
        public ActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Feedback>>(httpclient.GetStringAsync(uri).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: FeedBacksController/Details/5
        public ActionResult Details(int id)
        {
            var model = JsonConvert.DeserializeObject<Feedback>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return View(model);
        }

       

     

        // GET: FeedBacksController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = JsonConvert.DeserializeObject<Feedback>(httpclient.GetStringAsync(uri + id).Result);
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
