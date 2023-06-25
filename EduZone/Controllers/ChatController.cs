using EduZone.Models;
using Microsoft.AspNet.Identity;
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

            var reciver = context.Users.FirstOrDefault(c => c.Id == ReciverId);
            var sender = User.Identity.GetUserId();
            var asd = context.Chats.Where(c => c.SenderId == sender && c.ReciverId== ReciverId||(c.SenderId== ReciverId&&c.ReciverId== sender)).ToList();
            ViewBag.reciver = reciver;
            return View(asd);
        }
    }
}