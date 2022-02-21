using AspNetCoreHero.ToastNotification.Abstractions;
using ITMCollege.Areas.Admin.Models;
using ITMCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdmissionsController : Controller
    {
        private readonly INotyfService _notyf;
        public AdmissionsController(INotyfService noty)
        {
            this._notyf = noty;
        }
        private readonly string uriStream = "http://localhost:20646/api/streams/";
        private readonly string uriField = "http://localhost:20646/api/fields/";
        private readonly string uriAdmission = "http://localhost:20646/api/admissions/";
        HttpClient client = new HttpClient();
        // GET: AdmissionsController
        public ActionResult Index(int pg = 1)
        {
            var res = client.GetStringAsync(uriAdmission).Result;
            var list = JsonConvert.DeserializeObject<IEnumerable<AdmissionViewModel>>(res);
            var streamList = JsonConvert.DeserializeObject<IEnumerable<Stream>>
                            (client.GetStringAsync(uriStream).Result);
            ViewBag.StreamList = streamList;
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int rescCount = list.Count();
            var pager = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = list.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }

        // GET: AdmissionsController/Details/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var res = client.GetStringAsync(uriAdmission+id).Result;
            var data = JsonConvert.DeserializeObject<Admission>(res);
            ViewBag.StreamName = JsonConvert.DeserializeObject<Stream>(client.GetStringAsync(uriStream + data.StreamId).Result).StreamName;
            ViewBag.FieldName = JsonConvert.DeserializeObject<Field>(client.GetStringAsync(uriField + data.StreamId).Result).FieldName;
            return View(data);
        }

        // POST: AdmissionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, byte status,string RegNum)
        {
            try
            {
                var res = client.PutAsJsonAsync(uriAdmission+id+"?status="+status,status).Result;
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _notyf.Success($"Admission {RegNum} change status successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    _notyf.Error($"Admission {RegNum} change status fail.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                _notyf.Warning(e.Message);
                return View();
            }
        }
        // GET: AdmissionsController/Delete/5
        [HttpPost]
        public IEnumerable<Field> GetFields(int streamId)
        {
            var res = client.GetStringAsync(uriField + "GetFieldsByStreamId/" + streamId).Result;
            var data = JsonConvert.DeserializeObject<IEnumerable<Field>>(res);
            client.Dispose();
            return data;
        }
    }
}
