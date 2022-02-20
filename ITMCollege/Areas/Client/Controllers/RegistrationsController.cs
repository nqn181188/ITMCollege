using ITMCollege.Areas.Admin.Models;
using ITMCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Client.Controllers
{
    [Area("Client")]
    public class RegistrationsController : Controller
    {
        private readonly string uriAdmission = "http://localhost:20646/api/admissions/";
        private readonly string uriSpeSubject = "http://localhost:20646/api/spesubjects/";
        private readonly string uriOpSubject = "http://localhost:20646/api/opsubjects/";
        private readonly string uriResgistration = "http://localhost:20646/api/registrations/";
        HttpClient client = new HttpClient();
        //Get Registraion Controller
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.OpSubjectList = JsonConvert.DeserializeObject<IEnumerable<OpSubject>>(client.GetStringAsync(uriOpSubject).Result);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Registration reg,IFormFile file,string fullName)
        {
            try
            {
                string fileName = fullName.Replace(" ","")+"-"+Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/registration", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
                reg.Image = fileName;
                var res = client.PostAsJsonAsync(uriResgistration, reg).Result;
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["regstatus"] = 0;
                    return RedirectToAction("RegistrationNotification", new { regNum = reg.RegNum });
                }
                else
                {
                    TempData["regstatus"] = 1;
                    return RedirectToAction("RegistrationNotification", new { regNum = reg.RegNum });
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
        public ActionResult RegistrationNotification(string regNum)
        {
            if (TempData["regstatus"] != null)
            {
                ViewBag.RegStatus = TempData["regstatus"];
            }
            ViewBag.RegNum = regNum;
            return View();
        }
    }

}
