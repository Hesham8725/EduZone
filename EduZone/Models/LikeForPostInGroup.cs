using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{

    [Table("LikeForPostInGroup")]
    public class LikeForPostInGroup
    {
        public int Id { get; set; }

        [ForeignKey("PostInGroup")]
        public int PostId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public PostInGroup PostInGroup { get; set; }
    }
}