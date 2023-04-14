using System.Collections.Generic;

namespace ProjectAnswersAndQuestions.Models
{
    public class AnalyticsModel
    {
        public int CountUsers { get; set; }
        public int CountArticles { get; set; }
        public int CountComments { get; set; }
        public Dictionary<int, int> UsersAndArticle = new Dictionary<int, int>();
        public Dictionary<int, int> UsersAndComments = new Dictionary<int, int>();
        public string[] UsersIDArt; 
        public string[] UsersIDCom; 
        public string[] ArticlesCount; 
        public string[] CommentsCount; 
    }
}
