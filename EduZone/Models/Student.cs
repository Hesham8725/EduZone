using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public int Age { get; set; }

        public string Image { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }
       
        public int CollegeID { get; set; }

        public int GroupNo { get; set; }

        public double GPA { get; set; }

        public int Batch { get; set; }

        public string AccountID { get; set; }
        public string Department { get; set; }
    }
}