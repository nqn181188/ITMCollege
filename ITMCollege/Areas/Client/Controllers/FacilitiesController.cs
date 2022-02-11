using ITMCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Client.Controllers
{
    [Area("Client")]
    public class FacilitiesController : Controller
    {
    

        private readonly string uri = "http://localhost:20646/api/facilities/";
        private HttpClient httpclient = new HttpClient();

      
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
            return View();
        }

        // GET: FacilitiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FacilitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: FacilitiesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FacilitiesController/Edit/5
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

        // GET: FacilitiesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FacilitiesController/Delete/5
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
