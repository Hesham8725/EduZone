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
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }
     
    }
}