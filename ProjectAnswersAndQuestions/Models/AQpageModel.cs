using ProjectAnswersAndQuestions.DAL.Interfaces;
using System.Collections.Generic;

namespace ProjectAnswersAndQuestions.Models
{
    public class AQpageModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Article> ArticlesHot { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<UserRegistration> Users { get; set; }
        public Article Article { get; set; }
        public Comment Comment { get; set; }
        public UserRegistration AQuser { get; set; }
        public string Search { get; set; }

        public string SortView { get; set; } = "Hot";
        public List<CheckBoxOption> CheckboxOptions { get; set; } = new List<CheckBoxOption>
        {
                    new CheckBoxOption()
                    {
                        IsActive = true,                       
                        Value="Hot"                       
                    },
                    new CheckBoxOption()
                    {
                        IsActive = false,
                        Value="New"
                    },
                    new CheckBoxOption()
                    {
                        IsActive = false,
                        Value="Old"
                    }
        };
       
        

    }
}
