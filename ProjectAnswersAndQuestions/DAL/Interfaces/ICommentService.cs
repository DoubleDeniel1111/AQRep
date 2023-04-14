using Microsoft.EntityFrameworkCore;
using ProjectAnswersAndQuestions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ProjectAnswersAndQuestions.DAL.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllComments();
        Task AddComment(Comment comment);
        Task<int> AddArticleIDOnAnswerComment(Comment comment);
    }
}
