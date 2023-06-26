using EduZone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class PreRegistrationController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: PreRegistration
        public ActionResult Pre_Registration()
        {
            var currentUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(c => c.Id == currentUser);
            var student = context.GetStudents.FirstOrDefault(c => c.AccountID == currentUser);
            return View(student);
        }
    }
}