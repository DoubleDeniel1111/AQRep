using System.ComponentModel.DataAnnotations;

namespace ProjectAnswersAndQuestions.Models
{
    public class Role
    {
        [Required]
        [Key]
        public int RoleID { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string UserRole { get; set; }
    }
}
