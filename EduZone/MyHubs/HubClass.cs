using EduZone.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.MyHubs
{
    [HubName("HubClass")]
    public class HubClass : Hub
    {
        ApplicationDbContext context = new ApplicationDbContext();
     
        // like in group posts
       [HubMethodName("Addlike")]
       public void Addlike(string uid,int pid,bool flag)
        {
            int ok = 0;
            var query = context.LikeForPostInGroups.FirstOrDefault(i => i.PostId == pid && i.UserID == uid);
            var obj = new LikeForPostInGroup()
            {
                PostId = pid,
                UserID = uid
            };
            if (flag == false)
            {
                if (query == null)
                {
                    ok = 1;
                    context.LikeForPostInGroups.Add(obj);
                }
                else
                {
                    context.LikeForPostInGroups.Remove(query);
                    ok = 2;
                }
            }
            else
            {
                if (query != null)
                {
                ok = 2;
                context.LikeForPostInGroups.Remove(query);
                }
                else
                {
                    context.LikeForPostInGroups.Add(obj);
                    ok = 1;
                }
            }
            context.SaveChanges();
            string clr = ok==2 ? "secondary" : ok==1 ? "success": "";
            int numLikss = context.Likes.Where(s => s.PostId == pid).Count();
            Clients.All.NewLikeAdded(uid,pid, clr);
        }

        // like in timeline posts
        [HubMethodName("AddlikeInTimeLine")]
        public void AddlikeInTimeLine(string uid, int pid, bool flag)
        {
            int ok = 0;
            var query = context.Likes.FirstOrDefault(i => i.PostId == pid && i.UserID == uid);
            var obj = new Like()
            {
                PostId = pid,
                UserID = uid
            };
            if (flag == false)
            {
                if (query == null)
                {
                    ok = 1;
                    context.Likes.Add(obj);
                }
                else
                {
                    context.Likes.Remove(query);
                    ok = 2;
                }
            }
            else
            {
                if (query != null)
                {
                    ok = 2;
                    context.Likes.Remove(query);
                }
                else
                {
                    context.Likes.Add(obj);
                    ok = 1;
                }
            }
            context.SaveChanges();
            string clr = ok == 2 ? "secondary" : ok == 1 ? "success" : "";
            int numLikss = context.Likes.Where(s => s.PostId == pid).Count();
            Clients.All.NewLikeAddedInTimeLine(uid, pid, clr, numLikss);
        }
        [HubMethodName("AddComment")]
        public void AddComment(int postid,string message ,string userid)
        {
            var pst=context.Posts.FirstOrDefault(i=>i.Id== postid);
            var obj = new Comment()
            {
                Date = DateTime.Now,
                ContentOfComment = message,
                PostID = pst.Id,
                UserId = userid
            };
            context.Comments.Add(obj);
            context.SaveChanges();
            int numComents=context.Comments.Where(s => s.PostID == postid).Count();
            var name=context.Users.FirstOrDefault(i => i.Id == userid).Email.Split('.')[0];
            Clients.All.NewCommentAdded(message, name, obj.Id, numComents, postid);
        }
    }
}