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
    public class FacultiesController : Controller
    {
        private readonly string uri = "http://localhost:20646/api/faculties/";
        private readonly string uri2 = "http://localhost:20646/api/departments/";
        private HttpClient httpclient = new HttpClient();
        // GET: FacultiesController
        public ActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Faculty>>(httpclient.GetStringAsync(uri).Result);
            ViewBag.listdep = JsonConvert.DeserializeObject<IEnumerable<Department>>(httpclient.GetStringAsync(uri2).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: FacultiesController/Details/5
        public ActionResult Details(int id)
        {
            var listfacul = JsonConvert.DeserializeObject<IEnumerable<Faculty>>(httpclient.GetStringAsync(uri).Result);
            ViewBag.listfacul = listfacul.Where(o => o.DepId == id);
            return View();
        }

    }
}
