using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [StringLength(500, ErrorMessage = "Content must be between 1 and 500 characters", MinimumLength = 1)]
        public string ContentOfPost { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(50, ErrorMessage = "Author name must be between 1 and 50 characters", MinimumLength = 1)]
        [Display(Name = "Author")]
        public string AuthorName { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Likes")]
        public int Likes { get; set; }

        public List<Comment> Comments { get; set; }
    }
}