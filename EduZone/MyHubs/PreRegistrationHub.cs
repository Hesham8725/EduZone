using EduZone.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;

using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.MyHubs
{
    [HubName("PreRegistrationHub")]
    public class PreRegistrationHub:Hub
    {
        ApplicationDbContext context = new ApplicationDbContext();

        [HubMethodName("Add")]
        public void Add(int id,string UserId)
        {
            P_Registration p_Registration = new P_Registration();
            p_Registration.CourseId = id;
            p_Registration.UserId = UserId;
            context.P_Registrations.Add(p_Registration);
            context.SaveChanges();
 
            var course = context.GetCourses.FirstOrDefault(c => c.Id == id);
            Clients.Caller.clientAdd(course);
        }
    }
}