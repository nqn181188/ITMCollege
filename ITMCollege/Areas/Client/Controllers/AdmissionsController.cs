using ITMCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Client.Controllers
{
    [Area("Client")]
    public class AdmissionsController : Controller
    {
        private readonly string uriStream = "http://localhost:20646/api/streams/";
        HttpClient client = new HttpClient();
        // GET: AdmissionsController
        public ActionResult Index()
        {
            var res = client.GetStringAsync(uriStream).Result;
            var streamList = JsonConvert.DeserializeObject<IEnumerable<Stream>>(res);
            ViewBag.StreamList = streamList;
            return View();
        }

        // GET: AdmissionsController/Details/5
        

        // GET: AdmissionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdmissionsController/Create
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
    }
}
