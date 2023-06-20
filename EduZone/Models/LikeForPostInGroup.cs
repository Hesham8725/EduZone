using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{

    [Table("LikeForPostInGroup")]
    public class LikeForPostInGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        [ForeignKey("PostInGroup")]
        public int PostId { get; set; }

        public PostInGroup PostInGroup { get; set; }
    }
}