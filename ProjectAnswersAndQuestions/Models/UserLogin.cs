using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectAnswersAndQuestions.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Please enter Your email adress.")]
        [StringLength(500)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Your password")]
        public string UserPassword { get; set; }
    }
}
