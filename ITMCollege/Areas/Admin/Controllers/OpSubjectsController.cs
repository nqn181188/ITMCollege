using AspNetCoreHero.ToastNotification.Abstractions;
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
    public class OpSubjectsController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly string uriOpSubject = "http://localhost:20646/api/opsubjects/";
        private readonly string uriRegistration = "http://localhost:20646/api/registrations/";
        HttpClient client = new HttpClient();
        public OpSubjectsController(INotyfService notyf)
        {
            this._notyf = notyf;
        }
        // GET: OpSubjectsController
        public ActionResult Index(int pg=1)
        {
            var res = client.GetStringAsync(uriOpSubject).Result;
            var list = JsonConvert.DeserializeObject<IEnumerable<OpSubject>>(res);
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

        // GET: OpSubjectsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OpSubjectsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OpSubjectsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OpSubject subject)
        {
            if (subject == null)
            {
                return BadRequest();
            }
            try
            {
                var res = client.PostAsJsonAsync(uriOpSubject, subject).Result;
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _notyf.Success($"Optional Subject : {subject.SubjectName} - Inserted successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    _notyf.Error($"Optional Subject : {subject.SubjectName}  - Inserted faile. ");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                _notyf.Warning(e.Message);
                return View();
            }
        }
        // GET: OpSubjectsController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                var res = client.GetStringAsync(uriOpSubject + id).Result;
                var data = JsonConvert.DeserializeObject<OpSubject>(res);
                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            catch (Exception e)
            {
                _notyf.Warning(e.Message);
                return RedirectToAction("Index");
            }
            
        }
        // POST: OpSubjectsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OpSubject subject)
        {
            try
            {
                var res = client.PutAsJsonAsync(uriOpSubject + subject.SubjectId, subject).Result;
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _notyf.Success($"Optional Subject - Edited successfully.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Error($"Optional Subject - Edited fail.");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                _notyf.Warning(e.Message);
                return View();
            }
        }

        // GET: OpSubjectsController/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                var res = client.GetStringAsync(uriOpSubject + id).Result;
                var data = JsonConvert.DeserializeObject<OpSubject>(res);
                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            catch (Exception e)
            {
                _notyf.Warning(e.Message);
                return RedirectToAction("Index");
            }
        }
        // POST: OpSubjectsController/Delete/5
        public ActionResult Delete(int id, string SubjectName)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                var res = client.DeleteAsync(uriOpSubject + id).Result;
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _notyf.Success($"Optional Subject: {SubjectName} - Deleted successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    _notyf.Error($"Optional Subject: {SubjectName} - Deleted fail.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                _notyf.Warning(e.Message);
                return View();
            }
        }
    }
}
