using ClosedXML.Excel;
using ITMCollege.Areas.Admin.Models;
using ITMCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            ViewBag.StreamList = JsonConvert.DeserializeObject<IEnumerable<ITMCollege.Models.Stream>>(client.GetStringAsync(uriStream).Result);
            ViewBag.OpSubjectList = JsonConvert.DeserializeObject<IEnumerable<OpSubject>>(client.GetStringAsync(uriOpSubject).Result);
            var registartions = JsonConvert.DeserializeObject<IEnumerable<Registration>>(client.GetStringAsync(uriRegistration).Result);
            List<RegistrationViewModel> registrationViewModels = new();
            foreach(var item in registartions)
            {
                RegistrationViewModel reg = new RegistrationViewModel();
                GetRegistrationViewModelFormRegistration(reg, item);
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
                RegistrationViewModel data = new RegistrationViewModel();
                GetRegistrationViewModelFormRegistration(data, registration);
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
        public IActionResult DownloadExcelDocument(string searchRegNum, string searchName, int searchStream, int searchField, int searchSpeSubject, int searchOpSubject)
        {
            string contentTye = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Registration.xlsx";
            try
            {
                using(var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Registration");
                    worksheet.Cell(1, 1).Value = "No.";
                    worksheet.Cell(1, 2).Value = "Registration Number";
                    worksheet.Cell(1, 3).Value = "Full Name";
                    worksheet.Cell(1, 4).Value = "Data of Birth";
                    worksheet.Cell(1, 5).Value = "Gender";
                    worksheet.Cell(1, 6).Value = "Email";
                    worksheet.Cell(1, 7).Value = "Residential Address";
                    worksheet.Cell(1, 8).Value = "Permanent Address";
                    worksheet.Cell(1, 9).Value = "Stream";
                    worksheet.Cell(1, 10).Value = "Field";
                    worksheet.Cell(1, 11).Value = "Specialized Subject";
                    worksheet.Cell(1, 12).Value = "Optional Subject";
                    worksheet.Cell(1, 13).Value = "Emergency Name";
                    worksheet.Cell(1, 14).Value = "Emergency Phone";
                    worksheet.Cell(1, 15).Value = "Emergency Address";
                    var registartions = JsonConvert.DeserializeObject<IEnumerable<Registration>>(client.GetStringAsync(uriRegistration).Result);
                    List<RegistrationViewModel> list = new();
                    foreach (var item in registartions)
                    {
                        RegistrationViewModel reg = new();
                        reg = GetRegistrationViewModelFormRegistration(reg, item);
                        list.Add(reg);
                    }
                    if (!string.IsNullOrEmpty(searchName))
                    {
                        list = list.Where(r=>r.FullName.Contains(searchName)).ToList();
                    }
                    if (!string.IsNullOrEmpty(searchRegNum))
                    {
                        list = list.Where(r => r.FullName.Contains(searchRegNum)).ToList();
                    }
                    if (searchStream != 0)
                    {
                        list = list.Where(r => r.Stream.StreamId==searchStream).ToList();
                    }
                    if (searchField != 0)
                    {
                        list = list.Where(r => r.Field.FieldId== searchField).ToList();
                    }
                    if (searchSpeSubject != 0)
                    {
                        list = list.Where(r => r.SpeSubject.SubjectId== searchSpeSubject).ToList();
                    }
                    if (searchOpSubject != 0)
                    {
                        list = list.Where(r => r.OpSubject.SubjectId == searchOpSubject).ToList();
                    }
                    for (int i = 1; i <= list.Count; i++)
                    {
                        worksheet.Cell(i + 1, 1).Value = i;
                        worksheet.Cell(i + 1, 2).Value = list[i - 1].RegNum;
                        worksheet.Cell(i + 1, 3).Value = list[i - 1].FullName;
                        worksheet.Cell(i + 1, 4).Value = list[i - 1].DateOfBirth.ToShortDateString();
                        worksheet.Cell(i + 1, 5).Value = list[i - 1].Gender == true ? "Male" : "Female";
                        worksheet.Cell(i + 1, 6).Value = list[i - 1].Email;
                        worksheet.Cell(i + 1, 7).Value = list[i - 1].ResAddress;
                        worksheet.Cell(i + 1, 8).Value = list[i - 1].PerAddress;
                        worksheet.Cell(i + 1, 9).Value = list[i - 1].Stream.StreamName;
                        worksheet.Cell(i + 1, 10).Value = list[i - 1].Field.FieldName;
                        worksheet.Cell(i + 1, 11).Value = list[i - 1].SpeSubject.SubjectName;
                        worksheet.Cell(i + 1, 12).Value = list[i - 1].OpSubject.SubjectName;
                        worksheet.Cell(i + 1, 13).Value = list[i - 1].EmergencyName;
                        worksheet.Cell(i + 1, 14).Value = list[i - 1].EmergencyPhone;
                        worksheet.Cell(i + 1, 15).Value = list[i - 1].EmergencyAddress;
                    }
                    using(var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentTye, fileName);
                    }
                }
                
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
        }
        private RegistrationViewModel GetRegistrationViewModelFormRegistration (RegistrationViewModel reg, Registration item)
        {
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
            reg.SpeSubject = JsonConvert.DeserializeObject<SpeSubject>(client.GetStringAsync(uriSpeSubject + item.SpeSubjectId).Result);
            reg.OpSubject = item.OpSubjectId == null ? new OpSubject { SubjectId = 0, SubjectName = "None" } : JsonConvert.DeserializeObject<OpSubject>(client.GetStringAsync(uriOpSubject + item.OpSubjectId).Result);
            reg.EmergencyName = item.EmergencyName;
            reg.EmergencyPhone = item.EmergencyPhone;
            reg.EmergencyAddress = item.EmergencyAddress;
            return reg;
        }
    }
}
