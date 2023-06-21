using EduZone.Models.ViewModels;
using EduZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.IO;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.SignalR;
using EduZone.MyHubs;
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

        public ActionResult GetBatches()
        {
            return View();
        }
        public async Task<ActionResult> TimeLine()
        {
            User.Identity.GetUserId();
            var data = await context.Posts.OrderByDescending(x => x.Date).ToListAsync();
            return View(data);
        }

    }
}