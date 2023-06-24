using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Group code is required")]
        public string GroupCode { get; set; }
        [Required(ErrorMessage = "Form title is required")]
        public string FormTitle { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}