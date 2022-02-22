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

namespace ITMCollege.Areas.Client
{
    [Area("Client")]
    public class CoursesController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;

        private readonly string uri = "http://localhost:20646/api/courses/";
        private readonly string uri11 = "http://localhost:20646/api/fields/GetFieldsByStreamId/";
        //private readonly string uri1 = "http://localhost:20646/api/fields/";
        //private readonly string uri2 = "http://localhost:20646/api/streams/";
        private HttpClient httpclient = new HttpClient();

        public CoursesController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }
        // GET: CoursesController
        public ActionResult Index(int pg=1)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Course>>(httpclient.GetStringAsync(uri).Result);
            httpclient.Dispose();
            const int pageSize = 9;
            if (pg < 1)
                pg = 1;
            int rescCount = model.Count();
            var pager = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = model.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            //return View(model);
            return View(data);

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
            return Json(new SelectList(model, "FieldId", "FieldName"));
        }

       

        
    }
}
