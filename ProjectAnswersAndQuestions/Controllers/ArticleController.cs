//using ProjectAnswersAndQuestions;
//using ProjectAnswersAndQuestions.Models;
//using Dapper;
//using Microsoft.Data.SqlClient;
//using System.Data;
//using ProjectAnswersAndQuestions.DAL;
//using Microsoft.EntityFrameworkCore;
//using ProjectAnswersAndQuestions.DAL.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;

//namespace ProjectAnswersAndQuestions.Controllers
//{
//    public class ArticleController : Controller
//    {
//        private readonly IArticleService articleService;
//        private readonly ICommentService commentService;
//        private readonly IUserService userService;

//        public ArticleController(IArticleService articleService, ICommentService commentService, IUserService userService)
//        {
//            this.articleService = articleService;
//            this.commentService = commentService;
//            this.userService = userService; 
//        }
//        [HttpGet]
//        public async Task<IActionResult> NewArticle()
//        {
//            return View();
//        }
//        //[HttpGet]
//        public async Task<IActionResult> JoinArticle(AQpageModel aqpageModelOld, string search = null)
//        {
//            AQpageModel AQpageModel = new AQpageModel();
//            AQpageModel.Articles = await articleService.GetAllArticles();
//            AQpageModel.Article = await articleService.GetArticleByID(aqpageModelOld.Article.ArticleID);
//            AQpageModel.Comments = await commentService.GetAllComments();
//            if (User.Identity.IsAuthenticated)
//            {
//                AQpageModel.AQuser = await userService.GetUserByEmail(User.Identity.Name);
//            }
//            AQpageModel.Search = search;
//            return View(AQpageModel);
//        }
//    }
//}
