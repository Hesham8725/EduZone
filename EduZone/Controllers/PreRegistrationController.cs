using EduZone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            var department = context.GetDepartments.FirstOrDefault(c => c.Name.ToLower() == student.Department.ToLower());
            var ss = context.P_Registrations.Where(c => c.UserId == currentUser).ToList();
           // var courses = context.GetCourses.Where(c =>c.DepartmentId==department.Id).ToList();
            var time = DateTime.Now.Month;
            ViewBag.Year = DateTime.Now.Year;
            if ((time >= 9 && time <= 12) || time == 1 || time == 2)
            {
                ViewBag.Semester = "First";
            }
            else
            {
                ViewBag.Semester = "Second";

            }
            if (student.Batch == 1)
            {
                if ((time >= 9 && time <= 12) || time == 1 || time == 2)
                {
                ViewBag.courses =context.GetCourses.Where(c => c.Level == "First level" && c.Semester== "First semester").ToList();
                }
                else
                {
                ViewBag.courses = context.GetCourses.Where(c => c.Level == "First level" && c.Semester == "Second semester").ToList();

                }
            }
            else if(student.Batch == 2)
            {
                if ((time >= 9 && time <= 12) || (time == 1 && time == 2))
                {
                    ViewBag.courses = context.GetCourses.Where(c => c.Level == "Second level" && c.Semester == "First semester").ToList();
                }
                else
                {
                    ViewBag.courses = context.GetCourses.Where(c => c.Level == "Second level" && c.Semester == "Second semester").ToList();

                }
            }
            else if(student.Batch == 3|| student.Batch == 4)
            {
                ViewBag.courses = context.GetCourses.Where(c => c.DepartmentId == department.Id).ToList();
            }
            
            foreach(var item in ss)
            {
                var d = item.CourseId;
                var f = context.GetCourses.FirstOrDefault(c => c.Id == d);
                ViewBag.courses.Remove(f);
            }
         
            ViewBag.studentName = user.Name;
            ViewBag.studentId = student.ID;
            ViewBag.Department = student.Department;
            ViewBag.PhoneNumber = user.PhoneNumber;
            ViewBag.Email = user.Email;
            ViewBag.Batch = student.Batch;
            if (ss != null)
            {
                ViewBag.P_RegistrationsBefor = true;
            }
           
            return View(ss);
        }
    }
}