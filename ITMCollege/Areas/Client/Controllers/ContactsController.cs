using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Client.Controllers
{
    [Area("Client")]
    public class ContactsController : Controller
    {
        // GET: ContactsController
        public ActionResult Index()
        {
            return View();
        }

    }
}
