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
            return View();
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

        public ActionResult Exam()
        {
            List<Exam> exams = new List<Exam>();
            var ex = context.GetExams.Where(e => e.IsStart == true).ToList();
            var idx = User.Identity.GetUserId();
            var take = context.GetSudentExamDegrees.Where(e => e.StudentID == idx);
            foreach (var item in ex)
            {
                bool find = true;
                foreach (var item1 in take)
                {
                    
                    if (item.Id == item1.ExamID)
                    {
                        find = false;
                    } 
                }
                if (find == true)
                {
                    exams.Add(item);
                }
            }
            return View(exams);
        }
        public ActionResult OpenExam(int id)
        {
            var Exam = context.GetExams.FirstOrDefault(e => e.Id == id);
            return View(Exam);
        }
        [HttpPost]
        public ActionResult SaveExam(int id,string StudentName,string SNumber,string Student_ID)
        {
            var exam = context.GetExams.FirstOrDefault(e => e.Id == id);
            int Degre = 0;
            var Questions = context.GetQuestions.Where(e => e.ExamId == id).ToList();
            int N = Questions.Count();
            for (int i = 0; i < N; i++)
            {
                string v = Request.Form[$"QR{i}"].ToString();
                if (Request.Form[$"QR{i}"] != null&& Questions[i].CorrectAnswer == Request.Form[$"QR{i}"].ToString())
                {
                    Degre += Questions[i].Point;
                }
            }

            StudentExamDegree sudentExamDegree = new StudentExamDegree()
            {
                ExamID = id,
                Degree = Degre,
                GroupCode = exam.GroupCode,
                StudentID = Student_ID,
                StudentName = StudentName,
                Sitting_Number = SNumber
            };
            context.GetSudentExamDegrees.Add(sudentExamDegree);
            context.SaveChanges();

            //Send Email ?
            if (Request.Form["Send"]!=null&& Request.Form["Send"].ToString() == "1")
            {
                //Send code
            }

            // return to First Page
            List<Exam> exams = new List<Exam>();
            var idx = User.Identity.GetUserId();
            var ex = context.GetExams.Where(e => e.IsStart == true).ToList();
            var take = context.GetSudentExamDegrees.Where(e => e.StudentID == idx);
            foreach (var item in ex)
            {
                bool find = true;
                foreach (var item1 in take)
                {

                    if (item.Id == item1.ExamID)
                    {
                        find = false;
                    }
                }
                if (find == true)
                {
                    exams.Add(item);
                }
            }

            return RedirectToAction("Exam",exams);
        }
    }
}