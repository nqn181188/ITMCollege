using ITMCollege.Models;
using ITMCollege.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Client.Controllers
{
    [Area("Client")]
    public class AdmissionsController : Controller
    {
        private readonly string uriStream = "http://localhost:20646/api/streams/";
        private readonly string uriField = "http://localhost:20646/api/fields/";
        private readonly string uriAdmission = "http://localhost:20646/api/admissions/";
        HttpClient client = new HttpClient();
        private readonly IWebHostEnvironment _hostingEnvironment;
        // GET: AdmissionsController
        public ActionResult Index()
        {
            var res = client.GetStringAsync(uriStream).Result;
            var streamList = JsonConvert.DeserializeObject<IEnumerable<Stream>>(res);
            ViewBag.FieldList = GetFields(streamList.First().StreamId);
            ViewBag.StreamList = streamList;
            client.Dispose();
            return View();
        }

        // GET: AdmissionsController/Details/5
        

        // GET: AdmissionsController/Create
        
        // POST: AdmissionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admission admission)
        {
            try
            {
                string RegNum = GenerateRegNum();
                admission.RegNum = RegNum;
                var res = client.PostAsJsonAsync(uriAdmission, admission).Result;
                if (res.StatusCode==System.Net.HttpStatusCode.OK)
                {
                    client.Dispose();
                    TempData["admission_mess"] = "Congratulations, you have successfully admitted";
                    return RedirectToAction("AdmissionNotification",admission);
                }
                else
                {
                    client.Dispose();
                    TempData["admission_mess"] = "Admission fail, Please try again later.";
                    return RedirectToAction("AdmissionNotification", admission);
                }
                
            }
            catch
            {
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        public IEnumerable<Field> GetFields(int streamId)
        {
            var res = client.GetStringAsync(uriField + "GetFieldsByStreamId/"+streamId).Result;
            var data = JsonConvert.DeserializeObject<IEnumerable<Field>>(res);
            client.Dispose();
            return data;
        }
        public ActionResult AdmissionNotification(Admission admission)
        {
            if (TempData["admission_mess"] != null)
            {
                ViewBag.Mess = TempData["admission_mess"];
            }
            return View(admission);
        }
        public string GenerateRegNum()
        {
            var RegNum = new StringBuilder();
            RegNum.Append("ST");
            RegNum.Append(DateTime.Now.Year-2000);
            do
            {
                RegNum.Append(RandomString(6));
            } while (CheckRegNum(RegNum.ToString()));
            return RegNum.ToString();
        }
        public string RandomString(int length)
        {
            Random rd = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[rd.Next(s.Length)]).ToArray()) ;
        }
        public bool CheckRegNum(string regNum)
        {
            var checkRegNum = client.GetStringAsync(uriAdmission+ "CheckRegNumber/" + regNum).Result;
            if (checkRegNum.ToString() == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Obsolete]
        public IActionResult DownloadFile()
        {
            FileServices file = new FileServices(_hostingEnvironment);
            file.GetFile("test.txt");
            return RedirectToAction("Index");
        }
    }
}
