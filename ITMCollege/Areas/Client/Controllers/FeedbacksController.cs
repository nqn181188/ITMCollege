using ITMCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly string uri = "http://localhost:20646/api/feedbacks/";
        private HttpClient httpclient = new HttpClient();

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
                    httpclient.Dispose();
                    return RedirectToAction("Index");
                }
                else
                {
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
