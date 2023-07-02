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
        public JsonResult GetNumberOfUsers()
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
            
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            pairs["NumberOfAdmins"] = cntA;
            pairs["NumberOfStudents"] = cntS;
            pairs["NumberOfDoctors"] = cntD;
            pairs["NumberOfGroups"] = context.GetGroups.Count();
            pairs["NumberOfExams"] = context.GetExams.Count();

            return Json(pairs,JsonRequestBehavior.AllowGet);
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
    }
}