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

        private readonly string uriCourse = "http://localhost:20646/api/courses/";
        private readonly string uri = "http://localhost:20646/api/fields/GetFieldsByStreamId/";
        private readonly string uriField = "http://localhost:20646/api/fields/";
        private readonly string uriStream = "http://localhost:20646/api/streams/";

        private HttpClient httpclient = new HttpClient();

        public CoursesController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }
        // GET: CoursesController
        public ActionResult Index(int searchStream, int searchField, int page)
        {
            ViewBag.searchStream = searchStream;
            ViewBag.searchField = searchField;
            List<SelectListItem> streamList = new List<SelectListItem>();
            var streams = JsonConvert.DeserializeObject<IEnumerable<ITMCollege.Models.Stream>>(httpclient.GetStringAsync(uriStream).Result);
            streamList.Add(new SelectListItem { Text = "---Choose Stream---", Value = "0" });
            foreach (var item in streams)
            {
                streamList.Add(new SelectListItem { Text = $"{item.StreamName}", Value = $"{item.StreamId}" });
            }
            foreach (var item in streamList)
            {
                item.Selected = item.Value.Equals(searchStream.ToString()) ? true : false;
            }
            ViewBag.StreamList = streamList;

            var list = JsonConvert.DeserializeObject<IEnumerable<Course>>(httpclient.GetStringAsync(uriCourse).Result);
            foreach (var item in list)
            {
                item.Field = JsonConvert.DeserializeObject<Field>(httpclient.GetStringAsync(uriField + item.FieldId).Result);
            }

            if (searchStream != 0)
            {
                list = list.Where(a => a.StreamId == searchStream);
            }
            if (searchField != 0)
            {
                list = list.Where(a => a.FieldId == searchField);
            }


            const int pageSize = 6;
            page = page > 1 ? page : 1;
            int resCount = list.Count();
            var pager = new Pager(resCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var data = list.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            ViewBag.TotalPage = (int)resCount / pageSize + 1;
            return View(data);

        }


        //// GET: CoursesController
        //public ActionResult Index(int pg=1)
        //{
        //    var model = JsonConvert.DeserializeObject<IEnumerable<Course>>(httpclient.GetStringAsync(uriCourse).Result);
        //    httpclient.Dispose();
        //    const int pageSize = 9;
        //    if (pg < 1)
        //        pg = 1;
        //    int rescCount = model.Count();
        //    var pager = new Pager(rescCount, pg, pageSize);
        //    int recSkip = (pg - 1) * pageSize;
        //    var data = model.Skip(recSkip).Take(pager.PageSize).ToList();
        //    this.ViewBag.Pager = pager;
        //    //return View(model);
        //    return View(data);

        //}

        // GET: CoursesController/Details/5
        public ActionResult Details(int id)
        {
            var model = JsonConvert.DeserializeObject<Course>(httpclient.GetStringAsync(uriCourse + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        public JsonResult getfieldsFromDatabaseByStream(int id)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Field>>(httpclient.GetStringAsync(uri + id).Result);
            httpclient.Dispose();
            return Json(new SelectList(model, "FieldId", "FieldName"));
        }

       

        
    }
}
