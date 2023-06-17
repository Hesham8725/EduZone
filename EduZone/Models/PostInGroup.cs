using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace EduZone.Models
{

    [Table("PostInGroup")]   
    public class PostInGroup
    {
        public PostInGroup() 
        {
            LikeForPostInGroup = new List<LikeForPostInGroup>();
            CommentForPostInGroup = new List<CommentForPostInGroup>();
        }
      
        public int Id { get; set; }

        [Display(Name ="Contant Of Post")]
        [Required(AllowEmptyStrings =false,ErrorMessage = "Content is required")]
        [StringLength(maximumLength:500, ErrorMessage = "Content of post  must be between ( 3 : 500 )", MinimumLength = 3)]
        public string Contant { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }

        [Display(Name = "Create Date")]
        public DateTime Created { get; set; }

        [Display(Name = "Update Date")]
        public DateTime Updated { get; set; }

        //[ForeignKey("Group")]
        public int GroupId { get; set; }

        //public Group Group { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Likes")]
        public virtual List<LikeForPostInGroup> LikeForPostInGroup { get; set; }

        [Display(Name = "Comments")]
        public virtual List<CommentForPostInGroup> CommentForPostInGroup { get; set; }

    }
}