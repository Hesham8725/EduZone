using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    [Table("LikeForPostInTimeLine")]
    public class LikeForPostInTimeLine
    {
        public int Id { get; set; }

        [ForeignKey("PostInTimeLine")]
        public int PostId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public PostInTimeLine PostInTimeLine { get; set; }
    }
}