using EduZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class ChatController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: chat
        public ActionResult AllChat()
        {
            return View();
        }
        public ActionResult Chat_with_one(string ReciverId)
        {
            var student = context.Users.FirstOrDefault(c => c.Id == ReciverId);
           
            return View(student);
        }
    }
}