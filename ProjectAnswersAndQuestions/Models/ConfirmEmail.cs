using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectAnswersAndQuestions.Models
{
    public class ConfirmEmail
    {
        [Required(ErrorMessage = "Please enter Your Code")]
        public Guid ConfirmUserEmail { get; set; }   
    }
}
