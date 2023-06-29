using EduZone.Models.ViewModels;
using EduZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace EduZone.Services
{
    public class FormatOtherUser
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public List<ApplicationUser> OtherUsers(string userid)
        {
            var chatWithother = context.GetChatIndividual.Where(e => e.SenderId == userid).
                                                          Select(e => e.ReceiverId).
                                                          Distinct().ToList();
            chatWithother.AddRange(context.GetChatIndividual.Where(e => e.ReceiverId == userid).
                                                        Select(e => e.SenderId).
                                                        Distinct().ToList()
                                                        );
            chatWithother = chatWithother.Distinct().ToList();

            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var user in chatWithother)
            {
                var p = context.Users.FirstOrDefault(e => e.Id == user);
                users.Add(p);
            }
            return users;
        }

        public string FormatTimeOfLastMessage(DateTime time)
        {
            string timeFormat = "";
            if (DateTime.Now.Day - time.Day == 0
                    && DateTime.Now.Month - time.Month == 0
                    && DateTime.Now.Year - time.Year == 0)
            {
                timeFormat = time.ToString("h:mm tt");
            }
            else if (DateTime.Now.Day - time.Day == 1
                         && DateTime.Now.Month - time.Month == 0
                         && DateTime.Now.Year - time.Year == 0)
            {
                timeFormat = " Yestarday";
            }
            else
            {
                timeFormat = time.ToString("MM/dd/yyyy");
            }
            return timeFormat;
        }

        public List<UsersAndLastSeenViewModel> UsersAndLastSeens(List<ApplicationUser> users, List<LastMessageInChatIndividual> lastMessages,string currentUserId)
        {
            List<UsersAndLastSeenViewModel> usersAndLasts = new List<UsersAndLastSeenViewModel>();
            int cont = 0;
            for (int i = 0; i < users.Count; i++)
            {
                for (int j = 0; j < lastMessages.Count; j++)
                {
                    if (users[i].Id != currentUserId)
                    {
                        if (users[i].Id == lastMessages[j].ReseverID || users[i].Id == lastMessages[j].SendId)
                        {

                            var lastAnduser = new UsersAndLastSeenViewModel();
                            lastAnduser.Name = users[i].Name;
                            lastAnduser.Image = users[i].Image;
                            lastAnduser.Id = users[i].Id;
                            var id = users[i].Id;
                            var t = context.GetIsOnlines.FirstOrDefault(e => e.UserId == id);
                            if (t != null)
                            {
                                lastAnduser.OnLineOrNot = FormatTimeOfLastSeen(t.CreatedAt);
                            }
                            lastAnduser.TimeOfLastSeenTi = lastMessages[j].LastMessage;
                            lastAnduser.TimeOfLastSeenStr = FormatTimeOfLastMessage(lastMessages[j].LastMessage);
                            usersAndLasts.Add(lastAnduser);
                        }
                    }
                    else
                    {
                        if(cont == 0&& lastMessages[j].ReseverID== lastMessages[j].SendId)
                        {
                            var lastAnduser = new UsersAndLastSeenViewModel();
                            lastAnduser.Name = users[i].Name;
                            lastAnduser.Image = users[i].Image;
                            lastAnduser.Id = users[i].Id;
                            var id = users[i].Id;
                            var t = context.GetIsOnlines.FirstOrDefault(e => e.UserId == id);
                            if (t != null)
                            {
                                lastAnduser.OnLineOrNot = FormatTimeOfLastSeen(t.CreatedAt);
                            }
                            lastAnduser.TimeOfLastSeenTi = lastMessages[j].LastMessage;
                            lastAnduser.TimeOfLastSeenStr = FormatTimeOfLastMessage(lastMessages[j].LastMessage);
                            usersAndLasts.Add(lastAnduser);
                            cont++;
                        }
                    }                
                }
            } 
            usersAndLasts = usersAndLasts.OrderByDescending(x => x.TimeOfLastSeenTi).ToList();
            return usersAndLasts;

        }


        public string FormatTimeOfLastSeen(DateTime time)
        {
            string timeFormat = "";
            if (DateTime.Now.Day - time.Day == 0
                    && DateTime.Now.Month - time.Month == 0
                    && DateTime.Now.Year - time.Year == 0)
            {
                if (DateTime.Now.Hour - time.Hour == 0)
                {
                    if (DateTime.Now.Minute - time.Minute < 4)
                    {
                        timeFormat = "Online";
                    }
                    else
                    {
                        timeFormat = (DateTime.Now.Minute - time.Minute).ToString() + " minutes ago";
                    }
                }
                else
                {
                    timeFormat = time.ToString("h:mm tt");
                }
            }
            else if (DateTime.Now.Day - time.Day == 1
                         && DateTime.Now.Month - time.Month == 0
                         && DateTime.Now.Year - time.Year == 0)
            {
                timeFormat = " Yestarday";
            }
            else
            {
                timeFormat = time.ToString("MM/dd/yyyy");
            }
            return timeFormat;
        }

    }
}