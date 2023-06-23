using EduZone.Models;
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

namespace EduZone.Controllers
{
    public class EducatorController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Educator
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        public async Task<ActionResult> TimeLine()
        {
            User.Identity.GetUserId();
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
            var pst=context.Posts.FirstOrDefault(i=>i.Id==id);
            return View(pst);
        }

    }
}

