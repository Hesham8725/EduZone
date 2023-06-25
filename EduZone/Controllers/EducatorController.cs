using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class EducatorController : Controller
    {
        // GET: Educator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateExam()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult CreateExam(object )
        //{
        //    return View();
        //}
    }
}