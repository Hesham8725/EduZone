using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class Educator
    {
        [Key]
        public int ID { get; set; }


        [Required]
        public string AcademicDegree { get; set; }

        // for connect tables
        public string AccountID { get; set; }
    }
}