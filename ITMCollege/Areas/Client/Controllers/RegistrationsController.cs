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

namespace ITMCollege.Areas.Client.Controllers
{
    [Area("Client")]
    public class RegistrationsController : Controller
    {
        private readonly string uriStream = "http://localhost:20646/api/streams/";
        private readonly string uriField = "http://localhost:20646/api/fields/";
        private readonly string uriAdmission = "http://localhost:20646/api/admissions/";
        private readonly string uriSpeSubject = "http://localhost:20646/api/spesubjects/";
        private readonly string uriOpSubject = "http://localhost:20646/api/opsubjects/";
        private readonly string uriResgistration = "http://localhost:20646/api/registrations/";
        HttpClient client = new HttpClient();
        //Get Registraion Controller
        public ActionResult Index()
        {
            ViewBag.OpSubjectList = JsonConvert.DeserializeObject<IEnumerable<OpSubject>>(client.GetStringAsync(uriOpSubject).Result);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Registration reg,IFormFile file)
        {
            try
            {
                var res = client.PostAsJsonAsync(uriResgistration, reg).Result;
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RegistrationNotification(reg.RegNum, 1);
                }
                else
                {
                    return RegistrationNotification(reg.RegNum, 1);
                }
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult GetAdmissionInfor(string regNum)
        {
            var res = client.GetStringAsync(uriAdmission + "GetAdmissionByRegNum/" + regNum).Result;
            if (res.ToString()=="")
            {
                return Json(null);
            }
            else
            {
                var data = JsonConvert.DeserializeObject<Admissions>(res);
                var dic = new Dictionary<string, string>
                {
                    {"fullName",data.FullName },
                    {"dateOfBirth",data.DateOfBirth.ToShortDateString() },
                    {"gender",data.Gender==true?"true":"false"},
                    {"resAdd",data.ResAddress},
                    {"perAdd",data.PerAddress},
                    {"stream",data.Stream.StreamName},
                    {"field",data.Field.FieldName},
                    {"email",data.Email},
                    {"status",data.Status.ToString()},
                    {"fieldId",data.FieldId.ToString()},
                    {"regNum",data.RegNum},


                };
                return Json(dic);
            }
        }
        [HttpPost]
        public JsonResult GetSpeSubjectList(int fieldId)
        {
            var res = client.GetStringAsync(uriSpeSubject + "GetSpecialSubjectsByFieldId/" + fieldId).Result;
            var data = JsonConvert.DeserializeObject<IEnumerable<SpeSubject>>(res);
            return Json(data);
        }
        [HttpPost]
        public JsonResult GetOpSubjectList()
        {
            var res = client.GetStringAsync(uriOpSubject).Result;
            var data = JsonConvert.DeserializeObject<IEnumerable<OpSubject>>(res);
            return Json(data);
        }
        private ActionResult RegistrationNotification(string regNum, int regStatus)
        {

            return View();
        }
    }

}
