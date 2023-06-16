using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Models
{
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEducatorMail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEducatorMail(String Mail)
        {
            MailOfDoctors doctor = new MailOfDoctors();
            doctor.DoctorMail = Mail;
            context.MailOfDoctors.Add(doctor);
            context.SaveChanges();
            return Content("Add");
        }
        

    }
}