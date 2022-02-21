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
    public class RegistrationsController : Controller
    {
        private readonly string uriStream = "http://localhost:20646/api/streams/";
        private readonly string uriField = "http://localhost:20646/api/fields/";
        private readonly string uriAdmission = "http://localhost:20646/api/admissions/";
        private readonly string uriRegistration = "http://localhost:20646/api/registrations/";
        private readonly string uriSpeSubject = "http://localhost:20646/api/spesubjects/";
        private readonly string uriOpSubject = "http://localhost:20646/api/opsubjects/";
        HttpClient client = new HttpClient();
        // GET: RegistrationsController
        public ActionResult Index(int pg=1)
        {
            ViewBag.StreamList = JsonConvert.DeserializeObject<IEnumerable<Stream>>(client.GetStringAsync(uriStream).Result);
            var registartions = JsonConvert.DeserializeObject<IEnumerable<Registration>>(client.GetStringAsync(uriRegistration).Result);
            List<RegistrationViewModel> registrationViewModels = new();
            foreach(var item in registartions)
            {
                RegistrationViewModel reg = new RegistrationViewModel();
                reg.RegistrationId = item.RegistrationId;
                reg.RegNum = item.RegNum;
                var admissionInfor = JsonConvert.DeserializeObject<AdmissionViewModel>(client.GetStringAsync(uriAdmission + "GetAdmissionByRegNum/" + item.RegNum).Result);
                reg.FullName = admissionInfor.FullName;
                reg.Gender = admissionInfor.Gender;
                reg.DateOfBirth = admissionInfor.DateOfBirth;
                reg.ResAddress = admissionInfor.ResAddress;
                reg.PerAddress = admissionInfor.PerAddress;
                reg.Email = admissionInfor.Email;
                reg.Stream = admissionInfor.Stream;
                reg.Field = admissionInfor.Field;
                reg.Image = item.Image;
                reg.SpeSubject = JsonConvert.DeserializeObject<SpeSubject>(client.GetStringAsync(uriSpeSubject+item.SpeSubjectId).Result);
                if (item.OpSubjectId==0)
                {
                    reg.OpSubject = JsonConvert.DeserializeObject<OpSubject>(client.GetStringAsync(uriOpSubject + item.OpSubjectId).Result);
                }
                else
                {
                    reg.OpSubject = new OpSubject {
                        SubjectId = 0,
                        SubjectName="None"
                    };
                }
                reg.EmergencyName = item.EmergencyName;
                reg.EmergencyPhone = item.EmergencyPhone;
                reg.EmergencyAddress = item.EmergencyAddress;
                registrationViewModels.Add(reg);
            }
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int rescCount = registrationViewModels.Count();
            var pager = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = registrationViewModels.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }

        // GET: RegistrationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegistrationsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistrationsController/Create
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

        // GET: RegistrationsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegistrationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: RegistrationsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegistrationsController/Delete/5
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
        public IEnumerable<Field> GetFieldByStreamId(int StreamId)
        {
            var res = client.GetStringAsync(uriField + "GetFieldsByStreamId/" + StreamId).Result;
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
    }
}
