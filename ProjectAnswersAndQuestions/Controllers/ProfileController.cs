//using Microsoft.AspNetCore.Mvc;
//using ProjectAnswersAndQuestions.DAL.Interfaces;
//using ProjectAnswersAndQuestions.Models;
//using System.Threading.Tasks;

//namespace ProjectAnswersAndQuestions.Controllers
//{
//    public class ProfileController : Controller
//    {

//        private readonly IUserService userService;

//        public ProfileController(IUserService userService)
//        {
//            this.userService = userService;
//        }
//        [HttpGet]
//        public async Task<IActionResult> MyProfile()
//        {
//            var usersEmail = User.Identity.Name;
//            var user  = await userService.GetUserByEmail(usersEmail);
//            if (user!=null)
//            {
//                return View(user);
//            }
//            return View();
//            //var articles = await articleService.GetAllArticles();           
//        }
//    }
//}
