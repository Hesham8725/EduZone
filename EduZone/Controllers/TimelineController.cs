using EduZone.Models;
using EduZone.MyHubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class TimelineController : Controller
    {
        
        ApplicationDbContext context = new ApplicationDbContext();
        public async Task<ActionResult> TimeLine()
        {
            var data = await context.Posts.OrderByDescending(x => x.Date).ToListAsync();
            return View(data);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(Post post , HttpPostedFileBase File)
        {
            post.UserName = User.Identity.Name;
            post.UserId = User.Identity.GetUserId();
            post.Date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(TimeLine));
            }
            string PostImage = "";
            if (File != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(File.FileName));
                File.SaveAs(path);
                post.ImageUrl = File.FileName;
                PostImage = File.FileName;
            }
            context.Posts.Add(post);
            context.SaveChanges();

           

            var Profileimage = context.Users.Find(post.UserId).Image;
            var name = context.Users.Find(post.UserId).Name;
            var adminhubcontext = GlobalHost.ConnectionManager.GetHubContext<HubClass>();
            adminhubcontext.Clients.All.NewPostAdded(post,name,Profileimage,PostImage);

            return RedirectToAction(nameof(TimeLine));
        }
        public ActionResult ShowCommentOfPost(int id)
        {
            var pst = context.Posts.FirstOrDefault(i => i.Id == id);
            return View(pst);
        }
    }
}