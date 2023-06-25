using EduZone.Models;
//using EduZone.Models.Class;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class GroupController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Group
        public ActionResult Index()
        {
            List<Group> _groups = new List<Group>();
            string userid = User.Identity.GetUserId();
            
            var Groups = context.GetGroupsMembers.Where(e => e.MemberId == userid);
            if (Groups != null)
            {
                foreach (var item in Groups)
                {
                    var GName = context.GetGroups.FirstOrDefault(e => e.Code == item.GroupId);
                    _groups.Add(GName);
                }
            }
            return View(_groups);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string GroupName,string Description, HttpPostedFileBase file)
        {
            //Add Group
            Group group = new Group();
            group.GroupName = GroupName;
            group.Description = Description;
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Images"), fileName);
            file.SaveAs(path);
            group.image = file.FileName;
            group.DateOfCreate = DateTime.Now.Date;
            group.CreatorID = User.Identity.GetUserId();
            string codex = RandomGroupCode.GetCode();
            var len = context.GetGroups.Where(e => e.Code == codex);
            while (len.Count()!=0)
            {
                codex = RandomGroupCode.GetCode();
                len = context.GetGroups.Where(e => e.Code == codex);
            }
            group.Code = codex;
            context.GetGroups.Add(group);
            context.SaveChanges();

            //Add to memberGroup
            GroupsMembers GM = new GroupsMembers();
            GM.GroupId = codex;
            GM.IsCreate = true;
            GM.creationData = DateTime.Now;
            GM.MemberId = User.Identity.GetUserId();
            context.GetGroupsMembers.Add(GM);
            context.SaveChanges();

            //return to page of group
            return Content("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //From Index View
        public ActionResult JoinGroup(string CodeOfGroup)
        {
            string userId = User.Identity.GetUserId();
            var IsFound = context.GetGroups.FirstOrDefault(e=>e.Code == CodeOfGroup);
            if (IsFound != null)
            {

                var YouInGroup = context.GetGroupsMembers.Where(e => e.GroupId == CodeOfGroup && e.MemberId == userId).ToList();
                if (YouInGroup.Count != 0)
                {
                    return Content("You Are allredy Joined !");
                }
                else
                {
                    //Add to memberGroup
                    GroupsMembers GM = new GroupsMembers();
                    GM.GroupId = CodeOfGroup;
                    GM.IsCreate = false;
                    GM.MemberId = userId;
                    GM.creationData = DateTime.Now;
                    context.GetGroupsMembers.Add(GM);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            else
            {
                return Content("Not found");
            }
        }
        public ActionResult Group_Post(string GroupCode)
        {
            // first Get Group
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            ViewBag.GCR7 = GroupValue.CreatorID;

            return View();
        }
        public ActionResult Group_Material(string GroupCode)
        {
            // first Get Group
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            // GCR --> Group Creater
            // 7 --> Childhood love player
            ViewBag.GCR7 = GroupValue.CreatorID;

            var listOfMaterial = context.GetMaterials.Where(e => e.GroupCode == GroupCode).ToList();
            return View(listOfMaterial);
        }
        public ActionResult SaveMaterial(HttpPostedFileBase file,string GCode)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var find = context.GetMaterials.FirstOrDefault(e => e.Name == fileName);
                if (find != null)
                {
                    fileName += context.GetMaterials.Count().ToString();
                }
                var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);
                file.SaveAs(path);

                GroupMaterial group = new GroupMaterial()
                {
                    Name = fileName,
                    //Size = GetFileSize.Get(file),
                    //Type = GetTypeOfFile.Get(file),
                    GroupCode = GCode
                };
                context.GetMaterials.Add(group);
                context.SaveChanges();
            }
            return RedirectToAction("Group_Material",new { GroupCode = GCode });
        }
        public ActionResult DeleteMaterial(string GCode,int id)
        {
            var obj = context.GetMaterials.FirstOrDefault(e => e.ID == id);
            var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), obj.Name);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            context.GetMaterials.Remove(obj);
            context.SaveChanges();
            return RedirectToAction("Group_Material", new { GroupCode = GCode });
        }
        public ActionResult DownloadMaterial(string GCode, int id)
        {
            var obj = context.GetMaterials.FirstOrDefault(e => e.ID == id);
            var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), obj.Name);
            if (System.IO.File.Exists(path))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return File(fileBytes, "application/octet-stream", obj.Name);
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult Group_Member(string GroupCode)
        {
            
            // first Get Group
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            ViewBag.GCR7 = GroupValue.CreatorID;
            var GroupMembers = context.GetGroupsMembers.Where(c => c.GroupId == GroupCode).ToList();
            return View(GroupMembers);
        }
        public ActionResult Delete_Member(string id)
        {
            var ss = context.GetGroupsMembers.FirstOrDefault(c => c.MemberId == id);
            context.GetGroupsMembers.Remove(ss);
            context.SaveChanges();
            string code = TempData["GroupCode"].ToString();
            return RedirectToAction("Group_Member", new { GroupCode = code });
        }
        public ActionResult Group_Chat(string GroupCode)
        {
            // first Get Group
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            ViewBag.GCR7 = GroupValue.CreatorID;

            return View();
        }
        public ActionResult Group_About(string GroupCode)
        {
            // first Get Group
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            var s = GroupValue.CreatorID;
            var ss = context.Users.FirstOrDefault(c => c.Id == s);
            var members = context.GetGroupsMembers.Count(c => c.GroupId == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            ViewBag.GCR7 = GroupValue.CreatorID;

            return View();
        }
        public ActionResult LeaveGroup(string GroupCode)
        {
            var userId = User.Identity.GetUserId();
            var MyGroup = context.GetGroupsMembers.FirstOrDefault(e => e.GroupId == GroupCode && e.MemberId == userId);
            context.GetGroupsMembers.Remove(MyGroup);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteGroup(string GCode)
        {
            var userId = User.Identity.GetUserId();
            var Group = context.GetGroups.FirstOrDefault(e => e.Code == GCode && e.CreatorID == userId);
            if (Group != null)
            {
                var MembersINGroupe = context.GetGroupsMembers.Where(e => e.GroupId == GCode).ToList();
                foreach (var Member in MembersINGroupe)
                {
                    context.GetGroupsMembers.Remove(Member);
                    context.SaveChanges();
                }
                context.GetGroups.Remove(Group);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Group_Post", new { GroupCode = GCode });
        }
    }
}