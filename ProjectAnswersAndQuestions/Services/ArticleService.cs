using ProjectAnswersAndQuestions.DAL.Interfaces;
using ProjectAnswersAndQuestions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.Services
{
    public class ArticleService: IArticleService
    {
        private readonly IArticleRepository articlesRepository;

        public ArticleService(IArticleRepository articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        public async Task AddArticle(Article article)
        {
           await articlesRepository.AddArticle(article);
        }

        public async Task<IEnumerable<Article>> GetAllArticles(string sortView)
        {
            return await articlesRepository.GetAllArticles(sortView);
        }

        //public async Task<Article> GetArticleByID(int Id)
        //{
        //    return await articlesRepository.GetArticleByID(Id);
        //}
        public async Task DeleteArticle(Article article)
        {
            await articlesRepository.DeleteArticle(article);
        }
    }
}
