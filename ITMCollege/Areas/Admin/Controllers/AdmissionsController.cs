using AspNetCoreHero.ToastNotification.Abstractions;
using ITMCollege.Areas.Admin.Models;
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
        public ActionResult Index(string searchRegNum, int searchStream, int searchField, string searchStatus,int page)
        {
            ViewBag.searchRegNum = searchRegNum;
            ViewBag.searchStream = searchStream;
            ViewBag.searchField = searchField;
            ViewBag.searchStatus = searchStatus;
            List<SelectListItem> streamList = new List<SelectListItem>();
            var streams = JsonConvert.DeserializeObject<IEnumerable<Stream>>(client.GetStringAsync(uriStream).Result);
            streamList.Add(new SelectListItem { Text="---Choose Stream---",Value="0" });
            foreach (var item in streams)
            {
                streamList.Add(new SelectListItem { Text=$"{item.StreamName}",Value=$"{item.StreamId}"});
            }
            foreach(var item in streamList)
            {
                item.Selected=item.Value.Equals(searchStream.ToString())?true:false;
            }
            ViewBag.StreamList = streamList;
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem { Text = "---Choose Status---", Value = "" });
            statusList.Add(new SelectListItem { Text = "Waiting", Value = "0" });
            statusList.Add(new SelectListItem { Text = "Accepted", Value = "1" });
            statusList.Add(new SelectListItem { Text = "Rejected", Value = "2" });
            foreach (var item in statusList)
            {
                item.Selected = item.Value.Equals(searchStatus) ? true : false;
            }
            ViewBag.StatusList = statusList;
            var res = client.GetStringAsync(uriAdmission).Result;
            var list = JsonConvert.DeserializeObject<IEnumerable<AdmissionViewModel>>(res);
            foreach(var item in list)
            {
                item.Field = JsonConvert.DeserializeObject<Field>(client.GetStringAsync(uriField + item.FieldId).Result);
            }
            if (!string.IsNullOrEmpty(searchRegNum))
            {
                list = list.Where(a => a.RegNum.Contains(searchRegNum));
            }
            if (searchStream!=0)
            {
                list = list.Where(a => a.StreamId == searchStream);
            }
            if (searchField!=0)
            {
                list = list.Where(a => a.FieldId == searchField);
            }
            if (!string.IsNullOrEmpty(searchStatus))
            {
                list = list.Where(a => a.Status == Byte.Parse(searchStatus));
            }
            
            const int pageSize = 10;
            page = page>1?page:1;
            int resCount = list.Count();
            var pager = new Pager(resCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var data = list.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            ViewBag.TotalPage = (int)resCount / pageSize + 1;
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
