using EduZone.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class apiController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: api
        public JsonResult Numbers()
        {
            var Users = context.Users.ToList();
            int cntS = 0,cntD=0, cntA=0;
            foreach (var item in Users)
            {
                if (GetRole(item.Id) == "Student")
                {
                    cntS++;
                }
                else if (GetRole(item.Id) == "Educator")
                {
                    cntD++;
                }
                else
                {
                    cntA++;
                }
            }
            
            Dictionary<string, long> pairs = new Dictionary<string, long>();
            pairs["NumberOfAdmins"] = cntA;
            pairs["NumberOfStudents"] = cntS;
            pairs["NumberOfDoctors"] = cntD;
            pairs["NumberOfGroups"] = context.GetGroups.Count();
            pairs["NumberOfExams"] = context.GetExams.Count();
            var MG = context.GetMaterials;
            long ans = 0;
            foreach (var item in MG)
            {
                ans+= Get_Kb_Size(item.Size);
            }
            pairs["SizeOfDate"] = ans;
            return Json(pairs,JsonRequestBehavior.AllowGet);
        }
        public JsonResult StudentCourses()
        {
            var idx = User.Identity.GetUserId();
            List<String> CN = new List<string>();
            var course = context.GetP_Registrations.Where(e => e.UserId == idx);
            foreach (var item in course)
            {
                CN.Add(context.GetCourses.FirstOrDefault(e => e.Id == item.CourseId).CourseName);
            }
            return Json(CN, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StudentNumbers()
        {
            var idx = User.Identity.GetUserId();
            Dictionary<string, double> pairs = new Dictionary<string,double>();
            var x = context.GetStudents.FirstOrDefault(e => e.AccountID == idx).GPA;
            pairs["GPA"] = x;
            return Json(pairs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EducatorLogic1()
        {
            var idx = User.Identity.GetUserId();
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            var cources = context.GetCourses.Where(e => e.DoctorOfCourse == idx);
            foreach (var item in cources)
            {
                var x = item.Id;
                pairs[item.CourseName] = context.GetP_Registrations.Where(e => e.CourseId == x).Count();
            }
            return Json(pairs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EducatorLogic2()
        {
            var idx = User.Identity.GetUserId();
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            var G = context.GetGroups.Where(e => e.CreatorID == idx);
            foreach (var item in G)
            {
                var x = item.Code;
                pairs[item.GroupName] = context.GetGroupsMembers.Where(e => e.GroupId == x).Count();
            }
            return Json(pairs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BatchNumbers()
        {
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            pairs["Batch1"] = context.GetStudents.Where(e => e.Batch == 1).Count();
            pairs["Batch2"] = context.GetStudents.Where(e => e.Batch == 2).Count();
            pairs["Batch3"] = context.GetStudents.Where(e => e.Batch == 3).Count();
            pairs["Batch4"] = context.GetStudents.Where(e => e.Batch == 4).Count();
            return Json(pairs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PostNumber()
        {
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            var post = context.Posts;
            foreach (var item in post)
            {
                string s = GetDay(item.Date.ToString());
                if (pairs.ContainsKey(s))
                {
                    pairs[s]++;
                }
                else
                {
                    pairs[s] = 1;
                }
            }
            pairs.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            return Json(pairs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PostINGroupNumber()
        {
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            var post = context.PostInGroups;
            foreach (var item in post)
            {
                string s = GetDay(item.Date.ToString());
                if (pairs.ContainsKey(s))
                {
                    pairs[s]++;
                }
                else
                {
                    pairs[s] = 1;
                }
            }
            pairs.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            return Json(pairs, JsonRequestBehavior.AllowGet);
        }

        private string GetRole(string id)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Retrieve the user by user id
            var user = userManager.FindById(id);

            if (user != null)
            {
                // Retrieve all the roles for the user
                var roles = userManager.GetRoles(id);

                if (roles != null && roles.Count > 0)
                {
                    return roles[0];
                }
            }
            return "-1";
        }
        private string GetDay(string _date)
        {
            DateTime date = DateTime.Parse(_date);
            DayOfWeek dayOfWeek = date.DayOfWeek;
            return dayOfWeek.ToString();
        }
        private long Get_Kb_Size(string _size)
        {
            int size;
            string numericPart = _size.Split('.')[0];
            int.TryParse(numericPart, out size);
            if (_size.Contains("KB"))
            {
                return size;
            }
            else if(_size.Contains("MB"))
            {
                return size* 1024;
            }
            else
            {
                return size * 1024 * 1024;
            }
        }
    }
}