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
        public ActionResult Index(string searchRegNum, string searchName, int searchStream, int searchField,int searchSpeSubject, int searchOpSubject, int page)
        {
            ViewBag.searchRegNum = searchRegNum;
            ViewBag.searchName = searchName;
            ViewBag.searchStream = searchStream;
            ViewBag.searchField = searchField;
            ViewBag.searchSpeSubject = searchSpeSubject;
            ViewBag.searchOpSubject = searchOpSubject;
            ViewBag.StreamList = JsonConvert.DeserializeObject<IEnumerable<Stream>>(client.GetStringAsync(uriStream).Result);
            ViewBag.OpSubjectList = JsonConvert.DeserializeObject<IEnumerable<OpSubject>>(client.GetStringAsync(uriOpSubject).Result);
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
                reg.OpSubject = item.OpSubjectId == null ? new OpSubject { SubjectId = 0, SubjectName = "None" } : JsonConvert.DeserializeObject<OpSubject>(client.GetStringAsync(uriOpSubject + item.OpSubjectId).Result);
                reg.EmergencyName = item.EmergencyName;
                reg.EmergencyPhone = item.EmergencyPhone;
                reg.EmergencyAddress = item.EmergencyAddress;
                registrationViewModels.Add(reg);
            }
            if (!string.IsNullOrEmpty(searchRegNum))
            {
                registrationViewModels = registrationViewModels.Where(r => r.RegNum.Contains(searchRegNum)).ToList();
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                registrationViewModels = registrationViewModels.Where(r => r.FullName.Contains(searchName)).ToList();
            }
            if (searchStream != 0)
            {
                registrationViewModels = registrationViewModels.Where(r => r.Stream.StreamId == searchStream).ToList() ;
            }
            if (searchField != 0)
            {
                registrationViewModels = registrationViewModels.Where(r => r.Field.FieldId == searchField).ToList();
            }
            if (searchSpeSubject != 0)
            {
                registrationViewModels = registrationViewModels.Where(r => r.SpeSubject.SubjectId == searchSpeSubject).ToList() ;
            }
            if (searchOpSubject != 0)
            {
                registrationViewModels = registrationViewModels.Where(r => r.OpSubject.SubjectId == searchOpSubject).ToList();
            }
            const int pageSize = 10;
            page = page > 1 ? page : 1;
            int resCount = registrationViewModels.Count();
            var pager = new Pager(resCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var data = registrationViewModels.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            ViewBag.TotalPage = (int)resCount / pageSize + 1;
            return View(data);
        }

        // GET: RegistrationsController/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var registration = JsonConvert.DeserializeObject<Registration>(client.GetStringAsync(uriRegistration + id).Result);
            if (registration == null)
            {
                return NotFound();
            }
            else
            {
                var admissionInfor = JsonConvert.DeserializeObject<AdmissionViewModel>(client.GetStringAsync(uriAdmission + "GetAdmissionByRegNum/" + registration.RegNum).Result);
                RegistrationViewModel data = new RegistrationViewModel {
                    RegistrationId = registration.RegistrationId,
                    RegNum = registration.RegNum,
                    FullName = admissionInfor.FullName,
                    Gender = admissionInfor.Gender,
                    DateOfBirth = admissionInfor.DateOfBirth,
                    ResAddress = admissionInfor.ResAddress,
                    PerAddress = admissionInfor.PerAddress,
                    Email = admissionInfor.Email,
                    Stream = admissionInfor.Stream,
                    Field = admissionInfor.Field,
                    Image = registration.Image,
                    SpeSubject = JsonConvert.DeserializeObject<SpeSubject>(client.GetStringAsync(uriSpeSubject + registration.SpeSubjectId).Result),
                    OpSubject= registration.OpSubjectId == null?new OpSubject { SubjectId=0,SubjectName="None"}: JsonConvert.DeserializeObject<OpSubject>(client.GetStringAsync(uriOpSubject + registration.OpSubjectId).Result),
                    EmergencyName = registration.EmergencyName,
                    EmergencyPhone = registration.EmergencyPhone,
                    EmergencyAddress = registration.EmergencyAddress

                };
                return View(data);
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
        public IEnumerable<SpeSubject> GetSpecialSubjectsByFieldId(int FieldId)
        {
            var res = client.GetStringAsync(uriSpeSubject + "GetSpecialSubjectsByFieldId/" + FieldId).Result;
            var data = JsonConvert.DeserializeObject<IEnumerable<SpeSubject>>(res);
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
