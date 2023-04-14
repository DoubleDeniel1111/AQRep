using Microsoft.EntityFrameworkCore;
using ProjectAnswersAndQuestions.DAL.Interfaces;
using ProjectAnswersAndQuestions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.Services
{
    public class CommentService:ICommentService
    {

        private readonly ICommentRepository  commentsRepository;

        public CommentService(ICommentRepository commentssRepository)
        {
            this.commentsRepository = commentssRepository;
        }

        public async Task<int> AddArticleIDOnAnswerComment(Comment comment)
        {
            return await commentsRepository.AddArticleIDOnAnswerComment(comment);
        }

        public async Task AddComment(Comment comment)
        {
            await commentsRepository.AddComment(comment);
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await commentsRepository.GetAllComments();  
        }
        //public async Task DeleteComment(Comment comment)
        //{
        //    await commentsRepository.DeleteComment(comment);
        //}
    }
}
