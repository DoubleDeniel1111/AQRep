using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAnswersAndQuestions.Models
{
    public class Email
    {
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public UserRegistration UserRegistration { get; set; }

        [Required(ErrorMessage = "Please enter Your email adress.")]
        [StringLength(500)]
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}
