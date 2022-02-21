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

    public class SpeSubjectsController : Controller
    {
        private readonly INotyfService _notyf;
        public SpeSubjectsController(INotyfService noty)
        {
            this._notyf = noty;
        }
        private readonly string uriSpeSubject = "http://localhost:20646/api/spesubjects/";
        private readonly string uriStream = "http://localhost:20646/api/streams/";
        private readonly string uriField = "http://localhost:20646/api/fields/";
        HttpClient client = new HttpClient();

        // GET: SpeSubjectsController
        public ActionResult Index(int pg=1)
        {
            var res = client.GetStringAsync(uriSpeSubject).Result;
            var list = JsonConvert.DeserializeObject<IEnumerable<SpeSubjectViewModel>>(res);
            var streamList = JsonConvert.DeserializeObject<IEnumerable<Stream>>(client.GetStringAsync(uriStream).Result);
            ViewBag.StreamList = streamList;
            foreach(var item in list)
            {
                item.Field = JsonConvert.DeserializeObject<Field>(client.GetStringAsync(uriField+item.FieldId).Result);
                item.Stream= JsonConvert.DeserializeObject<Stream>(client.GetStringAsync(uriStream + item.Field.StreamId).Result);
            }
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

        // GET: SpeSubjectsController/Details/5
        

        // GET: SpeSubjectsController/Create
        public ActionResult Create()
        {
            var res = client.GetStringAsync(uriStream).Result;
            var streamList = JsonConvert.DeserializeObject<IEnumerable<Stream>>(res);
            ViewBag.StreamList = streamList;
            ViewBag.FieldList = JsonConvert.DeserializeObject<IEnumerable<Field>>(client.GetStringAsync(uriField + "GetFieldsByStreamId/" + streamList.First().StreamId).Result) ;
            return View();
        }

        // POST: SpeSubjectsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SpeSubject subject)
        {
            try
            {
                var res = client.PostAsJsonAsync(uriSpeSubject,subject).Result;
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _notyf.Success($"Specialized Subject: {subject.SubjectName} - Inserted successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    _notyf.Error($"Specialized Subject: {subject.SubjectName} - Inserted fail.");
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                _notyf.Warning(e.Message);
                return View();
            }
        }

        // GET: SpeSubjectsController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var res = client.GetStringAsync(uriSpeSubject+id).Result;
                    var data = JsonConvert.DeserializeObject<SpeSubject>(res);
                    SpeSubjectViewModel subject = new SpeSubjectViewModel();
                    if (data == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        subject = GetSpeSubjectsFromSpeSubject(subject, data);
                        ViewBag.StreamList = JsonConvert.DeserializeObject<IEnumerable<Stream>>(client.GetStringAsync(uriStream).Result);
                        ViewBag.FieldList = JsonConvert.DeserializeObject<IEnumerable<Field>>(client.GetStringAsync(uriField + "GetFieldsByStreamId/" + subject.Field.StreamId).Result);
                        return View(subject);
                    }

                }
                catch (Exception e)
                {
                    _notyf.Warning(e.Message);
                    return RedirectToAction("Index");
                }
            }
        }

        // POST: SpeSubjectsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SpeSubjectViewModel subject)
        {
            if (subject == null)
            {
                return BadRequest();
            }
            try
            {
                SpeSubject data = new SpeSubject();
                data.SubjectId = subject.SubjectId;
                data.SubjectName = subject.SubjectName;
                data.FieldId = subject.FieldId;
                var res = client.PutAsJsonAsync(uriSpeSubject+data.SubjectId,data).Result;
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _notyf.Success($"Specialized Subject - Edited successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    _notyf.Error($"Specialized Subject - Edited fail.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                _notyf.Warning(e.Message);
                return View();
            }
        }

        // GET: SpeSubjectsController/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var res = client.GetStringAsync(uriSpeSubject + id).Result;
                    var data = JsonConvert.DeserializeObject<SpeSubject>(res);
                    SpeSubjectViewModel subject = new SpeSubjectViewModel();
                    if (data == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        subject = GetSpeSubjectsFromSpeSubject(subject, data);
                        return View(subject);
                    }

                }
                catch (Exception e)
                {
                    _notyf.Warning(e.Message);
                    return RedirectToAction("Index");
                }
            }
        }

        // POST: SpeSubjectsController/Delete/5
        public ActionResult Delete(int id,string SubjectName)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                var res = client.DeleteAsync(uriSpeSubject + id).Result;
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _notyf.Success($"Specialized Subject: {SubjectName} - Deleted successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    _notyf.Error($"Specialized Subject: {SubjectName} - Deleted fail.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                _notyf.Warning(e.Message);
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
        private SpeSubjectViewModel GetSpeSubjectsFromSpeSubject(SpeSubjectViewModel subject, SpeSubject data)
        {
            subject.SubjectId = data.SubjectId;
            subject.SubjectName = data.SubjectName;
            subject.FieldId = data.SubjectId;
            subject.Field = JsonConvert.DeserializeObject<Field>(client.GetStringAsync(uriField + data.FieldId).Result);
            subject.Stream = JsonConvert.DeserializeObject<Stream>(client.GetStringAsync(uriStream + subject.Field.StreamId).Result);
            return subject;
        }
    }
}
