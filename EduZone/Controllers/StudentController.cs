using EduZone.Models.ViewModels;
using EduZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNet.Identity;

namespace EduZone.Controllers
{
    public class StudentController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: User
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            var student = context.GetStudents.FirstOrDefault(c => c.AccountID == currentUser);
            return View(student);
        }
        public ActionResult CourseEnrollment()
        {
            return View();
        }

        public ActionResult GetBatches(int BN=0)
        {
            ViewBag.BN = BN;
            var memberes = context.GetStudents.Where(e => e.Batch == BN);
            List<ApplicationUser> students = new List<ApplicationUser>();
            if (memberes != null)
            {
                foreach (var item in memberes)
                {
                    var val = context.Users.FirstOrDefault(e => e.Id == item.AccountID);
                    students.Add(val);
                }
            }
            return View(students);
        }
    }
}