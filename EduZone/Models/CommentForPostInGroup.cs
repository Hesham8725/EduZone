using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{

    [Table("CommentForPostInGroup")]
    public class CommentForPostInGroup
    {
        public int Id { get; set; }

        [ForeignKey("PostInGroup")]
        //[Display(Name ="Post")]
        public int PostId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Content of Comment is required")]
        [StringLength(maximumLength: 150, ErrorMessage = "Content of Comment must be between  ( 2 : 150 ) ", MinimumLength = 2)]
        public string Contant { get; set; }

        [Display(Name = "Create Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Created { get; set; }

        [Display(Name = "Update Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Updated { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public PostInGroup PostInGroup { get; set; }
    }
}