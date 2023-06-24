using EduZone.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Hubs
{
    [HubName("IndividuallyChat")]
    public class IndividuallyChat:Hub
    {
        [HubMethodName("SendMessage")]
        public void SendMessage(Chat chat)
        {
         
        }
    }
}