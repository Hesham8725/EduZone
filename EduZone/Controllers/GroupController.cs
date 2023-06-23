using EduZone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
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
        public ActionResult Index(string GroupName, string Description)
        {
            //Add Group
            Group group = new Group();
            group.GroupName = GroupName;
            group.Description = Description;
            group.DateOfCreate = DateTime.Now.Date;
            group.CreatorID = User.Identity.GetUserId();
            string codex = RandomGroupCode.GetCode();
            var len = context.GetGroups.Where(e => e.Code == codex);
            while (len.Count() != 0)
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
            var IsFound = context.GetGroups.FirstOrDefault(e => e.Code == CodeOfGroup);
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
                    GM.TimeGoin= DateTime.Now;
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

            string userId = User.Identity.GetUserId();
            var membar = context.GetGroupsMembers.FirstOrDefault(e => e.MemberId == userId);
            var datOfJoin = membar.TimeGoin;

            var chatGroup = context.GetChatGroups.OrderBy(e=>e.CreatedAt).Where(e => e.GroupName == GroupValue.GroupName &&e.CreatedAt>=datOfJoin).ToList();
            foreach (var item in chatGroup)
            {
                if (DateTime.Now.Day - item.CreatedAt.Day == 0 && DateTime.Now.Month - item.CreatedAt.Month == 0
                                        && DateTime.Now.Year - item.CreatedAt.Year == 0)
                {
                    item.time = item.CreatedAt.ToString("h: mm tt") + " Today";
                }
                else if (DateTime.Now.Day - item.CreatedAt.Day == 1
                                        && DateTime.Now.Month - item.CreatedAt.Month == 0
                                        && DateTime.Now.Year - item.CreatedAt.Year == 0)
                {
                    item.time = item.CreatedAt.ToString("h: mm tt") + " Yestarday";
                }
                else if (DateTime.Now.Day - item.CreatedAt.Day <= 7
                                        && DateTime.Now.Month - item.CreatedAt.Month == 0
                                        && DateTime.Now.Year - item.CreatedAt.Year == 0)
                {
                    item.time = item.CreatedAt.ToString("h: mm tt") + item.CreatedAt.DayOfWeek;
                }
                else
                {
                    item.time = item.CreatedAt.ToString("MM/dd/yyyy hh:mmtt");
                }
            }
            //chatGroup.Reverse();

            return View(chatGroup);
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
            var userId = User.Identity.GetUserId();
            var MyGroup = context.GetGroupsMembers.FirstOrDefault(e => e.GroupId == GroupCode && e.MemberId == userId);
            context.GetGroupsMembers.Remove(MyGroup);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteGroup(string GroupCode)
        {
            var userId = User.Identity.GetUserId();
            var Group = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode && e.CreatorID == userId);
            if (Group != null)
            {
                var MembersINGroupe = context.GetGroupsMembers.Where(e => e.GroupId == GroupCode).ToList();
                foreach (var Member in MembersINGroupe)
                {
                    context.GetGroupsMembers.Remove(Member);
                    context.SaveChanges();
                }
                context.GetGroups.Remove(Group);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Group_Post", new { GroupCode = GroupCode });
        }

    }
}