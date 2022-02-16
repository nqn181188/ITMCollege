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
        public ActionResult Index()
        {
            var res = client.GetStringAsync(uriAdmission).Result;
            var data = JsonConvert.DeserializeObject<IEnumerable<Admissions>>(res);
            var streamList = JsonConvert.DeserializeObject<IEnumerable<Stream>>
                            (client.GetStringAsync(uriStream).Result);
            ViewBag.StreamList = streamList;
            return View(data);
        }

        // GET: AdmissionsController/Details/5
        public ActionResult Edit(int id)
        {
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
            catch
            {
                return View();
            }
        }

        // GET: AdmissionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdmissionsController/Delete/5
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
