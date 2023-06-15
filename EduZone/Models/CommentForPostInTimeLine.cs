using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace EduZone.Models
{
    public class CommentForPostInTimeLine
    {
        public int Id { get; set; }

        [ForeignKey("PostInTimeLine")]
        public int PostId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Content of Comment is required")]
        [StringLength(maximumLength: 150, ErrorMessage = "Content of Comment must be between  ( 2 : 150 ) ", MinimumLength = 2)]
        public string Contant { get; set; }

        [Display(Name = "Create Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Created { get; set; }

        [Display(Name = "Update Date")]
        
        public DateTime Updated { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public PostInTimeLine PostInTimeLine { get; set; }
    }
}