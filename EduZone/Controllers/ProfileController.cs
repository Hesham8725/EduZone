using EduZone.Models;
using EduZone.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static EduZone.Controllers.ManageController;

namespace EduZone.Controllers
{
    public class ProfileController : Controller
    {
        private string codepass;
        ApplicationDbContext context = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ProfileController()
        {
        }

        public ProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Profile
        public ActionResult BasicInfo()
        {

            string id = GetUser();
            var user = context.Users.FirstOrDefault(e => e.Id == id);
            
            BasicInfo info = new BasicInfo()
            {
                Name = user.Name,
                NationalID = user.NationalID,
                Address = user.Address,
                Age = user.Age,
                Phone = user.PhoneNumber,
                Gender = user.Gender
            };
            return View(info);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BasicInfo(BasicInfo info)
        {
            if (ModelState.IsValid)
            {
                string id = GetUser();
                var user = context.Users.FirstOrDefault(e => e.Id == id);
                user.Name = info.Name;
                user.NationalID = info.NationalID;
                user.Address = info.Address;
                user.Age = info.Age;
                user.PhoneNumber = info.Phone;
                user.Gender = info.Gender;
                context.SaveChanges();
                ViewBag.Show = true;
            }
            return View(info);
        }

        public ActionResult CollegeInfo()
        {
            string id = GetUser();
            var user1 = context.GetStudents.FirstOrDefault(e => e.AccountID == id);
            if (user1 == null)
            {
                user1 = new Student();
                user1.AccountID = GetUser();
                context.GetStudents.Add(user1);
                context.SaveChanges();
            }
            CollegeInfo info = new CollegeInfo()
            {
                Batch = user1.Batch,
                GPA = user1.GPA,
                CollegeID = user1.CollegeID,
                GroupNo = user1.GroupNo
            };
            return View(info);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CollegeInfo(CollegeInfo info)
        {
            if (ModelState.IsValid)
            {
                string id = GetUser();
                var user1 = context.GetStudents.FirstOrDefault(e => e.AccountID == id);
                user1.Batch = info.Batch;
                user1.GPA = info.GPA;
                user1.GroupNo = info.GroupNo;
                user1.CollegeID = info.CollegeID;
                user1.Department = info.Department;
                user1.Section = info.Section;
                context.SaveChanges();
                ViewBag.Show = true;
            }
            return View(info);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                ViewBag.Show = true;
                return View();
            }
            AddErrors(result);
            return View(model);
        }

        public ActionResult ChangeImage()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeImage(HttpPostedFileBase ChangeImg)
        {

            string UserID = User.Identity.GetUserId();
            var Client = context.Users.FirstOrDefault(e => e.Id == UserID);
            string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(ChangeImg.FileName));
            ChangeImg.SaveAs(path);
            Client.Image = ChangeImg.FileName;
            ViewBag.Show = true;
            context.SaveChanges();
            return RedirectToAction("BasicInfo");
        }

        public ActionResult ChanageEmail()
        {
            string id = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(e => e.Id == id);
            ChanageEmail email = new ChanageEmail()
            {
                Email = user.Email
            };
            return View(email);
        }

        [HttpPost]
        public async Task<ActionResult> ChanageEmail(ChanageEmail chanageEmail,int num)
        {
            string id = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(e => e.Id == id);
            if (ModelState.IsValid)
            {
                
                if (num == 1)
                {
                    ViewBag.ShowCode = true;
                    codepass = RandomPasswordCode.GetCode();
                    SendEmail email = new SendEmail(codepass,1);
                   await email.SendEmailAsync(chanageEmail.NewEmail, null);
                }
                else if (num == 2)
                {
                    if(chanageEmail.code == 0)
                    {
                        ModelState.AddModelError("","Enter The code that Send to Email");
                        return View(chanageEmail);
                    }
                    if(chanageEmail.code == int.Parse(codepass))
                    {
                        user.Email = chanageEmail.Email;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Not Vaild Code");
                    }
                }
            }
            return View(chanageEmail);
        }

        public string GetUser()
        {
            string UserID = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    UserID = userIdClaim.Value;
                }
            }
            return UserID;
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}