using EduZone.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EduZone.Hubs
{
    [HubName("IndividuallyChat")]
    public class IndividuallyChat:Hub
    {
        ApplicationDbContext context = new ApplicationDbContext();
        //Dictionary<string, string> OnlineUser = new Dictionary<string, string>();
        [HubMethodName("SendMessage")]
        public void SendMessage(Chat chat)
        {
            string connectionId = Context.ConnectionId;
            context.Chats.Add(chat);
            context.SaveChanges();
            //asd = chat.SenderId;
            //Clients.Client(connectionId);
            Clients.Client(connectionId).sendAsync(chat.content);
          //  Clients.Client(connectionId).SendAsync();

        }
        //public override Task OnConnected()
        //{
            
        //    //هعمل insert
        //    var s = Context.ConnectionId;
        //    IndividuallyChat sasa = new IndividuallyChat();
            
            
        //    return null;
        //}
    }
}