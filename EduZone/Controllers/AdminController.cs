using EduZone.Models.ViewModels;
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

       
        //this function to get list of Mail Of Doctor
        private List<ExtraInfoOfMailOfDoctorViewModel> ListOfMailOfDoctor()
        {
            var mails = context.MailOfDoctors.ToList();
            var ExterInfo = new List<ExtraInfoOfMailOfDoctorViewModel>();
            foreach (var mail in mails)
            {
                var ex = new ExtraInfoOfMailOfDoctorViewModel();
                var nameOfDoc = context.Users.FirstOrDefault(e => e.Email == mail.DoctorMail);
                if (nameOfDoc != null)
                {
                    ex.DoctorName = nameOfDoc.Name;
                }
                ex.DoctorMail = mail.DoctorMail;
                ex.ID = mail.ID;
                ExterInfo.Add(ex);
            }
            return ExterInfo;
        }

        public ActionResult AddEducatorMail()
        {
            var ExterInfo = ListOfMailOfDoctor();
            return View(ExterInfo);
        }

        [HttpPost]
        public ActionResult AddEducatorMail(string email)
        {
            if (email != "") 
            {
                var Mail = context.MailOfDoctors.FirstOrDefault(e => e.DoctorMail == email);
                if (Mail != null)
                {
                    ViewBag.mailExist = "The email already exists";
                    var ExterInfo = ListOfMailOfDoctor();
                    return View(ExterInfo);
                }
                MailOfDoctors doctor = new MailOfDoctors();
                doctor.DoctorMail = email;
                context.MailOfDoctors.Add(doctor);
                context.SaveChanges();
            }

            return RedirectToAction("AddEducatorMail");
        }

        public ActionResult UpdateEducatorMail(int mailId)
        {
            if(TempData["mailExist"]!=null)
            {
                ViewBag.mailExist = TempData["mailExist"];
            }
            ViewBag.curd = "update";
            ViewBag.mail = context.MailOfDoctors.FirstOrDefault(e => e.ID == mailId);
            var ExterInfo = ListOfMailOfDoctor();
            return View("AddEducatorMail", ExterInfo);
        }

        [HttpPost]
        public ActionResult UpdateEducatorMail(int mailId,string email)
        {
            var ExterInfo = ListOfMailOfDoctor();
            var Mail = context.MailOfDoctors.FirstOrDefault(e => e.ID == mailId);
            if(Mail != null)
            {
                if(email != "")
                {
                    var EmailExist = context.MailOfDoctors.FirstOrDefault(e => e.DoctorMail == email);
                    if (Mail.DoctorMail != email)
                    {
                        if (EmailExist == null)
                        {
                            Mail.DoctorMail = email;
                            context.SaveChanges();
                            return RedirectToAction("AddEducatorMail");
                        }
                        TempData["mailExist"] = "The email after Update is already exists";
                        return RedirectToAction("UpdateEducatorMail", new { mailId = mailId });
                    }
                    else
                    {
                        TempData["mailExist"] = "Email and e-mail after Update are the same";

                        return RedirectToAction("UpdateEducatorMail", new { mailId = mailId });
                    }
                }
            }
            ViewBag.curd = "update";
            ViewBag.mail = context.MailOfDoctors.FirstOrDefault(e => e.ID == mailId);
            return View("AddEducatorMail", ExterInfo);
        }

        //public ActionResult DeleteEducatorMail(int mailId)
        //{
        //    ViewBag.curd = "delete";
        //    ViewBag.mail = context.MailOfDoctors.FirstOrDefault(e => e.ID == mailId);
        //    var ExterInfo = ListOfMailOfDoctor();
        //    return View("AddEducatorMail", ExterInfo);
        //}

        //[HttpPost]
        public ActionResult DeleteEducatorMail(int mailId)  {
            var Mail = context.MailOfDoctors.FirstOrDefault(e => e.ID == mailId);
            if (Mail != null)
            {
                context.MailOfDoctors.Remove(Mail);
                context.SaveChanges();
                return RedirectToAction("AddEducatorMail");
            }
            ViewBag.curd = "delete";
            ViewBag.mail = context.MailOfDoctors.FirstOrDefault(e => e.ID == mailId);
            var ExterInfo = ListOfMailOfDoctor();
            return View("AddEducatorMail", ExterInfo);
        }
    }
}