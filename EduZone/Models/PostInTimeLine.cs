using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace EduZone.Models
{
    public class PostInTimeLine
    {
        public PostInTimeLine() 
        {
            LikeForPostInTimeLines = new List<LikeForPostInTimeLine>();
            CommentForPostInGroup = new List<CommentForPostInGroup>();
        }
        public int Id { get; set; }

        [Display(Name = "Contant Of Post")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Content is required")]
        [StringLength(maximumLength: 500, ErrorMessage = "Content of post  must be between ( 3 : 500 )", MinimumLength = 3)]
        public string Contant { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }

        [Display(Name = "Create Date")]
       //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Created { get; set; }

        [Display(Name = "Update Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Updated { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Likes")]
        public virtual List<LikeForPostInTimeLine> LikeForPostInTimeLines { get; set; } 

        [Display(Name = "Comments")]
        public virtual List<CommentForPostInGroup> CommentForPostInGroup { get; set; }

    }
}