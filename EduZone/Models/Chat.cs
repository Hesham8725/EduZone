using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class Chat
    {
        public int ID { get; set; }
        public string SenderId { get; set; }
        public string ReciverId { get; set; }
        public string content { get; set; }

    }
}