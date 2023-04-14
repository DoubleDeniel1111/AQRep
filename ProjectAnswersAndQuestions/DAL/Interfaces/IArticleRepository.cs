using ProjectAnswersAndQuestions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.DAL.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllArticles(string sortView);
        Task AddArticle(Article article);
        Task DeleteArticle(Article article);

    }

}
