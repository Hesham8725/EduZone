using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Question text is required")]
        public string QuestionText { get; set; }
        public ICollection<AnswerChoice> AnswerChoices { get; set; }
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

    }
}