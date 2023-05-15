
using Catharsis.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProjectAnswersAndQuestions.DAL.Interfaces;
using ProjectAnswersAndQuestions.Models;
using ProjectAnswersAndQuestions.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.Controllers
{
    public class AnalyticsController : Controller
    {
        public IUserService userService;
        public IArticleService articleService;
        public ICommentService commentService;
        public AnalyticsController(IUserService userService, IArticleService articleService, ICommentService commentService)
        {
            this.userService = userService;
            this.articleService = articleService;
            this.commentService = commentService;   
        }
        public async Task<IActionResult> Analytics()
        {
           
            AnalyticsModel analytics = new AnalyticsModel ();
            string sqlExpression1 = "SELECT COUNT(*) FROM Users";
            string sqlExpression2 = "SELECT COUNT(*) FROM Comments";
            string sqlExpression3 = "SELECT COUNT(*) FROM Articles";
            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-CKVJ0S3\\SQLEXPRESS;Database=AQDB;Persist Security Info=False; MultipleActiveResultSets=True; Trusted_Connection=True; TrustServerCertificate=true"))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(sqlExpression1, connection);             
                SqlCommand command2 = new SqlCommand(sqlExpression2, connection);             
                SqlCommand command3 = new SqlCommand(sqlExpression3, connection);                        
                analytics.CountUsers = (int)command1.ExecuteScalar();
                analytics.CountComments = (int)command2.ExecuteScalar();
                analytics.CountArticles = (int)command3.ExecuteScalar();

                var users = await userService.GetAllUsers();
                var articles = await articleService.GetAllArticles("Hot");
                var comments = await commentService.GetAllComments();

                analytics.UsersIDArt = new string[analytics.CountUsers];
                analytics.ArticlesCount = new string[analytics.CountArticles];
                analytics.CommentsCount = new string[analytics.CountComments];

                int userCounter = 0;
                int articleCounter = 0;
                int commentCounter = 0;
                int countArticleOfUsers = 0;
                int countCommentsOfUsers = 0;

                foreach (var user in users)
                {
                    int count = 0;
                    foreach (var article in articles)
                    {
                      count=user.UserID == article.UserID ? 1 : 0;
                        if (count == 1)  break; 
                    }
                    countArticleOfUsers = count != 0 ? (int)new SqlCommand($"SELECT Count(ArticleID)" +
                                                                          $" FROM Articles, Users " +
                                                                          $"WHERE {user.UserID} = Articles.UserID  " +
                                                                          $"GROUP BY Users.UserID", connection).ExecuteScalar() : 0;
                    analytics.UsersAndArticle.Add(user.UserID,countArticleOfUsers);
                  
                }
                var SortedUsersAndArticle = (from UserID in analytics.UsersAndArticle orderby UserID.Value descending select UserID).Take(5).ToDictionary(pair => pair.Key, pair => pair.Value);
                analytics.UsersAndArticle = SortedUsersAndArticle;
                foreach (var item in analytics.UsersAndArticle)
                {
                    analytics.UsersIDArt[userCounter] = item.Key.ToString();
                    analytics.ArticlesCount[articleCounter] = item.Value.ToString();
                    userCounter++;
                    articleCounter++;
                }



                 userCounter = 0;
                 articleCounter = 0;
                 commentCounter = 0;
                 countArticleOfUsers = 0;
                 countCommentsOfUsers = 0;
                analytics.UsersIDCom = new string[analytics.CountUsers];
                foreach (var user in users)
                {
                    int count = 0;
                    foreach (var comment in comments)
                    {
                        count = user.UserID ==comment.UserID ? 1 : 0;
                        if (count == 1) break;
                    }
                    countCommentsOfUsers = count != 0 ? (int)new SqlCommand($"SELECT Count(CommentID)" +
                                                                          $" FROM Users, Comments " +
                                                                          $"WHERE {user.UserID} = Comments.UserID  " +
                                                                          $"GROUP BY Users.UserID", connection).ExecuteScalar() : 0;
                    analytics.UsersAndComments.Add(user.UserID, countCommentsOfUsers);

                }
                var SortedUsersAndComments = (from UserID in analytics.UsersAndComments orderby UserID.Value descending select UserID).Take(5).ToDictionary(pair => pair.Key, pair => pair.Value);
                analytics.UsersAndComments = SortedUsersAndComments;
                foreach (var item in analytics.UsersAndComments)
                {
                    analytics.UsersIDCom[userCounter] = item.Key.ToString();
                    analytics.CommentsCount[commentCounter] = item.Value.ToString();
                    userCounter++;
                    commentCounter++;
                }


            }

            return View(analytics);
        }
    }
}
