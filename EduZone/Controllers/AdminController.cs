using EduZone.MyHubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult Profile()
        {
            return View();
        }
        public async Task<ActionResult> TimeLine()
        {
            var data = await context.Posts.OrderByDescending(x => x.Date).ToListAsync();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(Post post)
        {
            post.UserName = User.Identity.Name;
            post.UserId = User.Identity.GetUserId();
            post.Date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(TimeLine));
            }

            context.Posts.Add(post);
            context.SaveChanges();

            var adminhubcontext = GlobalHost.ConnectionManager.GetHubContext<HubClass>();
            adminhubcontext.Clients.All.NewPostAdded(post);

            return RedirectToAction(nameof(TimeLine));
        }
        public ActionResult ShowCommentOfPost(int id)
        {
            var pst = context.Posts.FirstOrDefault(i => i.Id == id);
            return View(pst);
        }
    }
}