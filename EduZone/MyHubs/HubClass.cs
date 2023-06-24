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
        public void Addlike(string uid, int pid, bool flag)
        {
            if (flag == false)
            {
                var obj = new LikeForPostInGroup()
                {
                    PostId = pid,
                    UserID = uid

                };
                var query = context.LikeForPostInGroups.FirstOrDefault(i => i.PostId == pid && i.UserID == uid);
                if (query == null)
                    context.LikeForPostInGroups.Add(obj);
            }
            else
            {
                var query = context.LikeForPostInGroups.FirstOrDefault(i => i.PostId == pid && i.UserID == uid);
                if (query != null)
                    context.LikeForPostInGroups.Remove(query);
            }
            context.SaveChanges();
            string clr = flag ? "secondary" : "primary";
            Clients.All.NewLikeAdded(uid, pid, clr);
        }

        // like in timeline posts
        [HubMethodName("AddlikeInTimeLine")]
        public void AddlikeInTimeLine(string uid, int pid, bool flag)
        {
            if (flag == false)
            {
                var obj = new Like()
                {
                    PostId = pid,
                    UserID = uid

                };
                var query = context.Likes.FirstOrDefault(i => i.PostId == pid && i.UserID == uid);
                if (query == null)
                    context.Likes.Add(obj);
            }
            else
            {
                var query = context.Likes.FirstOrDefault(i => i.PostId == pid && i.UserID == uid);
                if (query != null)
                    context.Likes.Remove(query);
            }
            context.SaveChanges();

            string clr = flag ? "secondary" : "primary";
            Clients.All.NewLikeAddedInTimeLine(uid, pid, clr);
        }
        [HubMethodName("AddComment")]
        public void AddComment(int postId, string message, string userid)
        {
            var pst = context.Posts.FirstOrDefault(i => i.Id == postId);
            var obj = new Comment()
            {
                Date = DateTime.Now,
                ContentOfComment = message,
                PostID = pst.Id,
                UserId = userid
            };
            context.Comments.Add(obj);
            context.SaveChanges();
            int numComents = context.Comments.Where(s => s.PostID == postId).Count();
            var name = context.Users.FirstOrDefault(i => i.Id == userid).Name;
            var image = context.Users.FirstOrDefault(i => i.Id == userid).Image;

            Clients.All.NewCommentAdded(message, name, obj.Id, numComents , image ,postId);

        }
        public void DeleteComment(int id)
        {
            var com = context.Comments.Find(id);
            context.Comments.Remove(com);
            context.SaveChanges();
            int numComents = context.Comments.Where(s => s.PostID == id).Count();
            Clients.All.NewCommentDeleted(id.ToString(), numComents);
        }
    }
}