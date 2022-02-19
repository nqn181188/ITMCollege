using AspNetCoreHero.ToastNotification.Abstractions;
using ITMCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Client.Controllers
{
    [Area("Client")]
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
        // GET: FeedbacksController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FeedbacksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FeedbacksController/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: FeedbacksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback)
        {
            try
            {
                var response = httpclient.PostAsJsonAsync<Feedback>(uri, feedback).Result;
                if (response.IsSuccessStatusCode)
                {
                    _notyf.Success("Submit Succesfully");
                    httpclient.Dispose();
                    return RedirectToAction("Index");
                }
                else
                {
                    _notyf.Success("Submit Failed");
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: FeedbacksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeedbacksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FeedbacksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeedbacksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
