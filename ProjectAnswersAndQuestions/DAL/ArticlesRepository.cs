using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectAnswersAndQuestions.DAL.Interfaces;
using ProjectAnswersAndQuestions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.DAL
{
    public class ArticlesRepository: IArticleRepository
    {
        private readonly AppDbContext context;
        private readonly IUserService userService;
        private readonly ICommentService commentService;
        public ArticlesRepository(AppDbContext context,IUserService userService,ICommentService commentService)
        {
            this.context = context;
            this.userService = userService;
            this.commentService = commentService;
        }

        public async Task AddArticle(Article article)
        {
            if (article.Photo == null)
            {
                await context.Database.ExecuteSqlRawAsync($"INSERT INTO Articles VALUES ('{article.Author}','{article.UserID}','{article.ArticleName}','{article.ArticleСontent}','{article.ArticleCreateDate}',null)");
                //await context.Articles.AddAsync(article); 
                await context.SaveChangesAsync();
            }
            else
            {
                await context.Articles.AddAsync(article); 
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Article>> GetAllArticles(string sortView)
        {
            foreach (var article in context.Articles)
            {             
                article.Author = (await userService.GetUserById(article.UserID)).UserLogin;               
            }
            await context.SaveChangesAsync();         
            Task<List<Article>> articles=null ;           
            switch (sortView)
            {
                case "Hot":              
                    articles = context.Articles.FromSqlRaw($"SELECT * FROM Articles A2 ORDER BY (SELECT  COUNT(C.CommentID) FROM Articles A, Comments C WHERE C.ArticleID = A.ArticleID AND A2.ArticleID = A.ArticleID GROUP BY A.ArticleID)DESC ").ToListAsync();
                    break;
                case "New":
                    articles = context.Articles.FromSqlRaw($"SELECT * FROM Articles A2 ORDER BY A2.ArticleCreateDate DESC ").ToListAsync();
                    break;
                case "Old":
                    articles = context.Articles.FromSqlRaw($"SELECT * FROM Articles A2 ORDER BY A2.ArticleCreateDate ").ToListAsync();
                    break;
            }            
            return await articles;           
        }
       
        public async Task DeleteArticle(Article article)
        {
            //context.Articles.Remove(article);
            await context.Database.ExecuteSqlRawAsync($"DELETE FROM Articles WHERE ArticleID =  {article.ArticleID}");
            await context.SaveChangesAsync();
        }
    }
}
