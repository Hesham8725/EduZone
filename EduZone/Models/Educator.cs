﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    [Table("Educator")]
    public class Educator
    {
        [Key]
        [Required, MaxLength(14)]
        public string NationalID { get; set; }

        [Required, RegularExpression("[a-zA-Z]{3,}", ErrorMessage = "Name must be character only ")]
        public string Name { get; set; }

        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60 years")]
        public int Age { get; set; }

        [Required, RegularExpression("([^\\s]+(\\.(?i)(jpe?g|png|gif|bmp))$)")]
        public string Image { get; set; }

        [Required, RegularExpression("(?:m|M|f|F$")]
        public char Gender { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string AcademicDegree { get; set; }
        //public string Address { get; set; }

        // for connect tables
        public string AccountID { get; set; }
    }
}