using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAnswersAndQuestions.Models
{
    public class Article
    {
        [Required]
        [Key]
        public int ArticleID { get; set; }

        [Required]
        public string Author { get; set; }


        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public UserRegistration userRegistration { get; set; }


        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string ArticleName { get; set; }

        [Required]
        [StringLength(9999,MinimumLength = 2)]
        public string ArticleСontent { get; set; }

        [Required]
        public DateTime ArticleCreateDate { get; set; }

        public byte[] Photo { get; set; }
        [NotMapped]
        public IFormFile FilePhoto { get; set; }



    }
}
