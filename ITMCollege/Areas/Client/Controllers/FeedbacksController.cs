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
        public ActionResult Index(string status)
        {
            ViewBag.googlemap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.319281359242!2d106.66408561428717!3d10.786840061952285!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752ed2392c44df%3A0xd2ecb62e0d050fe9!2sFPT-Aptech%20Computer%20Education%20HCM!5e0!3m2!1sen!2s!4v1644572104262!5m2!1sen!2s";
            if (status == "hanoi")
            {
                ViewBag.googlemap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d59591.768547594744!2d105.49204537910158!3d21.013250000000003!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31345b465a4e65fb%3A0xaae6040cfabe8fe!2zVHLGsOG7nW5nIMSQ4bqhaSBI4buNYyBGUFQ!5e0!3m2!1svi!2s!4v1645291501321!5m2!1svi!2s";
            }
     
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
