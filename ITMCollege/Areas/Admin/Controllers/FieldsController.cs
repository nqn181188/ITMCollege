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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FieldsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;

        private readonly string uri = "http://localhost:20646/api/courses/GetCoursesByFieldId/";
        private readonly string uriField = "http://localhost:20646/api/fields/";
        private readonly string uriStream = "http://localhost:20646/api/streams/";
        private HttpClient httpclient = new HttpClient();

        public FieldsController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }
        // GET: FieldsController
        public ActionResult Index(int searchStream, int searchField, int page)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.searchStream = searchStream;
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

            var list = JsonConvert.DeserializeObject<IEnumerable<Field>>(httpclient.GetStringAsync(uriField).Result);
          

            if (searchStream != 0)
            {
                list = list.Where(a => a.StreamId == searchStream);
            }
          


            const int pageSize = 6;
            page = page > 1 ? page : 1;
            int resCount = list.Count();
            var pager = new Pager(resCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var data = list.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            ViewBag.TotalPage = (int)resCount / pageSize + 1;
            httpclient.Dispose();
            return View(data);

          
          
        }

        [HttpPost]
        public IEnumerable<Field> GetFieldByStreamId(int StreamId)
        {
            var res = httpclient.GetStringAsync(uriField + "GetFieldsByStreamId/" + StreamId).Result;
            var data = JsonConvert.DeserializeObject<IEnumerable<Field>>(res);
            if (data == null)
            {
                return null;
            }
            else
            {
                return data;
            }
        }
        // GET: FieldsController/Details/5
        public ActionResult Details(int id)
        {
            var model = JsonConvert.DeserializeObject<Field>(httpclient.GetStringAsync(uriField + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // GET: FieldsController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ListStream = JsonConvert.DeserializeObject<IEnumerable<ITMCollege.Models.Stream>>(httpclient.GetStringAsync(uriStream).Result);
            httpclient.Dispose();
            return View();
        }

        // POST: FieldsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FieldId,FieldName,StreamId")] Field field)
        {
            try
            {
               
                var data = httpclient.PostAsJsonAsync<Field>(uriField, field).Result;
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

        // GET: FieldsController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ListStream = JsonConvert.DeserializeObject<IEnumerable<ITMCollege.Models.Stream>>(httpclient.GetStringAsync(uriStream).Result);
            var model = JsonConvert.DeserializeObject<Field>(httpclient.GetStringAsync(uriField + id).Result);
            httpclient.Dispose();
            return View(model);
        }

        // POST: FieldsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FieldId,FieldName,StreamId")] Field field)
        {
            try
            {
                if (field != null)
                {
                    _notyf.Success("Edit Succesfully");
                    var model = httpclient.PutAsJsonAsync(uriField + id, field).Result;
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

        // GET: FieldsController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Course>>(httpclient.GetStringAsync(uri + id).Result);
            if (model.Count() < 1)
            {
                var data = JsonConvert.DeserializeObject<Field>(httpclient.GetStringAsync(uriField + id).Result);
                return View(data);
            }
            else
            {
                _notyf.Warning("Cant delete this record right now");
                return RedirectToAction(nameof(Index));
            }

           
        }

        // POST: FieldsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _notyf.Success("Delete Succesfully");
                var data = httpclient.DeleteAsync(uriField + id).Result;
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
