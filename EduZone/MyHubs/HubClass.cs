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
       /* [HubMethodName("Add")]
        public void Add()
        {
            Clients.All.NewPostAdded();
        }*/
       [HubMethodName("Addlike")]
       public void Addlike(string uid,int pid,bool flag)
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
                if(query!=null)
                context.LikeForPostInGroups.Remove(query);
            }
            context.SaveChanges();
            string clr = flag ? "secondary" : "primary";
            Clients.All.NewLikeAdded(uid,pid, clr);
        }
    }
}