using ProjectAnswersAndQuestions;
using ProjectAnswersAndQuestions.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using ProjectAnswersAndQuestions.DAL;
using Microsoft.EntityFrameworkCore;
using ProjectAnswersAndQuestions.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ProjectAnswersAndQuestions.Controllers
{
    public class AQpageController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICommentService commentService;
        private readonly IUserService userService;

        public AQpageController(IArticleService articleService, ICommentService commentService, IUserService userService)
        {
            this.articleService = articleService;
            this.commentService = commentService;
            this.userService = userService;
        }
     

        public async Task<IActionResult> AQpage(AQpageModel aqpageModelOld, int? articelID, string search = null)
        {

            AQpageModel AQpageModel = new AQpageModel();
            AQpageModel.Articles = await articleService.GetAllArticles(aqpageModelOld.SortView);
            AQpageModel.ArticlesHot = await articleService.GetAllArticles("Hot");
            AQpageModel.Users = await userService.GetAllUsers();
            if (aqpageModelOld.Article != null)
            {
                AQpageModel.Article = (await articleService.GetAllArticles(aqpageModelOld.SortView)).FirstOrDefault(article => article.ArticleID == aqpageModelOld.Article.ArticleID);
            }
            if (articelID != null)
            {
                AQpageModel.Article = (await articleService.GetAllArticles(aqpageModelOld.SortView)).FirstOrDefault(article => article.ArticleID == articelID);
            }
            AQpageModel.Comments = await commentService.GetAllComments();
            AQpageModel.Search = search;
            if (User.Identity.IsAuthenticated)
            {
                AQpageModel.AQuser = await userService.GetUserByEmail(User.Identity.Name);
            }

            //AQpageModel.Search = null;
            return View(AQpageModel);

        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Logout", "User");
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            return RedirectToAction("MyProfile", "Profile");
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfileInfo(UserRegistration AQuser)
        {
            if (ModelState.IsValid)
            {


                if (AQuser.Email != null && AQuser.UserLogin != null)
                {
                    var user = await userService.GetUserByEmail(User.Identity.Name);
                    AQuser.Avatar = user.Avatar;
                    if (AQuser.FileAvatar != null)
                    {
                        byte[] imageData = null;
                        // считываем переданный файл в массив байтов
                        using (var binaryReader = new BinaryReader(AQuser.FileAvatar.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)AQuser.FileAvatar.Length);
                        }
                        // установка массива байтов
                        AQuser.Avatar = imageData;
                    }
                    AQuser.UserID = (await userService.GetUserByEmail(User.Identity.Name)).UserID;
                    AQuser.UserPassword = (await userService.GetUserByEmail(User.Identity.Name)).UserPassword;
                    AQuser.UserConfirmPassword = (await userService.GetUserByEmail(User.Identity.Name)).UserConfirmPassword;

                    if (User.Identity.Name == AQuser.Email)
                    {
                        await userService.UpdateUser(AQuser);
                        return RedirectToAction("AQpage", "AQpage");
                    }
                    else if (await userService.GetUserByData(AQuser) == null)
                    {
                        await userService.UpdateUser(AQuser);
                        return RedirectToAction("Logout", "User");
                    }
                    else
                    {
                        return RedirectToAction("AQpage", "AQpage");
                    }
                }            
                else
                {
                    return RedirectToAction("AQpage", "AQpage");
                }
            }
            else
            {
                return RedirectToAction("AQpage", "AQpage");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PublishArticle(Article article)
        {
            if (article.FilePhoto != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(article.FilePhoto.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)article.FilePhoto.Length);
                }
                article.Photo = imageData;
            }
            if (article.ArticleСontent != null)
            {
                article.Author = User.Identity.Name;
                article.ArticleCreateDate = System.DateTime.Now;
                article.UserID = (await userService.GetUserByEmail(User.Identity.Name)).UserID;
                await articleService.AddArticle(article);
            }

            return RedirectToAction("AQpage", "AQpage");
        }

        public async Task<IActionResult> DeleteArticle(Article article)
        {
            if (article != null)
            {
                await articleService.DeleteArticle(article);
            }
            return RedirectToAction("AQpage", "AQpage");
        }

        [HttpPost]
        public async Task<IActionResult> PublishComment(Comment comment)
        {
            if (comment.ComentContent != null)
            {
                comment.Author = User.Identity.Name;
                comment.UserID = (await userService.GetUserByEmail(User.Identity.Name)).UserID;
                await commentService.AddComment(comment);
            }
          
            return RedirectToAction("AQpage", new { articelID = comment.ArticleID });
        }

        [HttpPost]
        public async Task<IActionResult> CommentOnComment(Comment comment)
        {
            if (comment.ComentContent != null)
            {
                comment.Author = User.Identity.Name;
                comment.UserID = (await userService.GetUserByEmail(User.Identity.Name)).UserID;
                comment.ArticleID = await commentService.AddArticleIDOnAnswerComment(comment);
                await commentService.AddComment(comment);
            }
            else
            {
                comment.ArticleID = await commentService.AddArticleIDOnAnswerComment(comment);
            }
            return RedirectToAction("AQpage", new { articelID = comment.ArticleID });
        }
        
    }
}