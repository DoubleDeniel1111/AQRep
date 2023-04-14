using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAnswersAndQuestions.Models
{

    public class UserRegistration
    {

        [Required]
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please enter Your Login (min 2 letter).")]
        [StringLength(50, MinimumLength = 2)]
        public string UserLogin { get; set; }

        [Required(ErrorMessage = "Please enter Your email adress.")]
        [StringLength(500)]
        [EmailAddress]
        public string Email { get; set; }
        //[ForeignKey("Email")]
        //public Email UserEmail { get; set; }

        [Required(ErrorMessage = "Please enter Your password")]
        public string UserPassword { get; set; }

        [Required]
        public int RoleID { get; set; } = 2;
        [ForeignKey("RoleID")]
        public Role Role { get; set; } 

        
        ////////////////////
        [Required]
        [Compare("UserPassword")]
        public string UserConfirmPassword { get; set; }

        public byte[] Avatar { get; set; }
        [NotMapped]
        public IFormFile FileAvatar { get; set; }
        [NotMapped]
        public bool modelIsValid { get; set; } = true;




    }

}

