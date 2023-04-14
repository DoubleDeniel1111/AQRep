using Microsoft.EntityFrameworkCore;
using ProjectAnswersAndQuestions.DAL.Interfaces;
using ProjectAnswersAndQuestions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.DAL
{
    public class CommentsRepository:ICommentRepository
    {
       private readonly AppDbContext context;
        private readonly IUserService userService;
        public CommentsRepository(AppDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService; 
        }

        public async Task<int> AddArticleIDOnAnswerComment(Comment comment)
        {
            return (await context.Comments.FromSqlRaw
                ($"SELECT * FROM Comments")
                .FirstOrDefaultAsync(x=>x.CommentID==comment.AnswerCommentID))
                .ArticleID;
        }

        public async Task AddComment(Comment comment)
        {
            await context.Database.ExecuteSqlRawAsync($"INSERT INTO Comments " +
                $"VALUES ('{comment.AnswerCommentID}','{comment.ComentContent}','{comment.ArticleID}','{comment.Author}','{comment.UserID}'a)");
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            foreach (var comment in context.Comments)
            {
                comment.Author = (await userService.GetUserById(comment.UserID)).UserLogin;
            }
            await context.SaveChangesAsync();
            var comments = context.Comments.FromSqlRaw("SELECT * FROM Comments").ToListAsync();
            return await comments;
        }
        //public async Task DeleteComment(Comment comment)
        //{
        //    context.Comments.Remove(comment);   
        //    await context.SaveChangesAsync();
        //}
    }
}
