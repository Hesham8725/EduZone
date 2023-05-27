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
        public async Task<ActionResult> TimeLine()
        {
            var data = await context.Posts.OrderByDescending(x => x.Date).ToListAsync();
           /* var adminhubcontext = GlobalHost.ConnectionManager.GetHubContext<HubClass>();
            adminhubcontext.Clients.All.NewPostAdded(post);
*/
            return View(data);
        }

    }
}