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
       /* [HubMethodName("Add")]
        public void Add()
        {
            Clients.All.NewPostAdded();
        }*/
    }
}