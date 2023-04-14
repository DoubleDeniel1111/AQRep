using ProjectAnswersAndQuestions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.DAL.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAllArticles(string sortView);
        //Task<IEnumerable<Article>> GetAllArticles(string searchString);
        //Task<Article> GetArticleByID(int Id);
        Task AddArticle(Article article);
        Task DeleteArticle(Article article);
    }
}
