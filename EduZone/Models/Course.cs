using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    [Table("Course")]
    public class Course
    {
        public int Id { get; set; }

        [Display(Name = "Course ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Name  of Course")]
        public string CourseName { get; set; }

        [Display(Name = "Description ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Description of Course")]
        public string Description { get; set; }

        [Display(Name = "Doctor Of Course ")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "Select doctor of Course")]
        public string DoctorOfCourse { get; set; }
        [Display(Name = "Hour")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Number of Hours of Course")]
        public int NumberOfHours { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}