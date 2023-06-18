using EduZone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
        public ActionResult Index(string GroupName,string Description)
        {
            //Add Group
            Group group = new Group();
            group.GroupName = GroupName;
            group.Description = Description;
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
                if (YouInGroup.Count!=0)
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
                    context.GetGroupsMembers.Add(GM);
                    context.SaveChanges();

                    return Content("joined");
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

            return View();
        }
        public ActionResult Group_Member(string GroupCode)
        {
            // first Get Group
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            ViewBag.GCR7 = GroupValue.CreatorID;

            return View();
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
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            ViewBag.GCR7 = GroupValue.CreatorID;

            return View();
        }

        public ActionResult LeaveGroup(string GroupCode)
        {
            return View();
        }
        public ActionResult DeleteGroup(string GroupCode)
        {
            return View();
        }

    }
}