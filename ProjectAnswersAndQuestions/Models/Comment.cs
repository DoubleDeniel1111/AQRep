using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAnswersAndQuestions.Models
{
    public class Comment
    {
        [Required]
        [Key]
        public int CommentID { get; set; }
        public int AnswerCommentID { get; set; }      

        [Required]
        [StringLength(9999, MinimumLength = 2)]
        public string ComentContent { get; set; }

        [Required]
        public int ArticleID { get; set; }
        [ForeignKey("ArticleID")]
        public Article Article { get; set; }    
        

        [Required]
        public string Author { get; set; }



        [Required]       
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public UserRegistration userRegistration { get; set; }

    }
}
