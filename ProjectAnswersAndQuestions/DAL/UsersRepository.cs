using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectAnswersAndQuestions.DAL;
using ProjectAnswersAndQuestions.DAL.Interfaces;
using ProjectAnswersAndQuestions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.DAL
{
    public class UsersRepository:IUserRepository
    {
        private readonly AppDbContext context;
        public UsersRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<int> AddUser(UserRegistration entity)
        {
            await context.Database.ExecuteSqlRawAsync($"INSERT INTO Users VALUES ('{entity.UserLogin}','{entity.Email}','{entity.UserPassword}','{entity.RoleID}','{entity.UserConfirmPassword}',null)");
            await context.SaveChangesAsync();
            return entity.UserID;
        }
        public async Task<UserRegistration> GetUserByData(UserLogin user)
        {
            var _user = await context.Users.FromSqlRaw("SELECT * FROM Users").
                                            FirstOrDefaultAsync(x => x.Email == user.Email && x.UserPassword ==user.UserPassword);
            return _user;
        }
        public async Task<UserRegistration> GetUserByData(UserRegistration user)
        {
            var _user = await context.Users.FromSqlRaw("SELECT * FROM Users")
                                           .FirstOrDefaultAsync(x => x.Email == user.Email);
            return _user;
        }
        public async Task<UserRegistration> GetUserByEmail(string email)
        {
            var user =await context.Users.FromSqlRaw("SELECT * FROM Users")
                                         .FirstOrDefaultAsync(user=>user.Email==email);
            return user;
        }
        public async Task<UserRegistration> GetUserById(int id)
        {
            UserRegistration userRegistration = new UserRegistration();

           userRegistration = await context.Users.FromSqlInterpolated($"SELECT * FROM Users")
                                                 .FirstOrDefaultAsync(user=>user.UserID==id);
            return  userRegistration;

        }
        public async Task UpdateUser(UserRegistration user)
        {
            
            await context.Database.ExecuteSqlRawAsync($"UPDATE Users " +
                $"                                      SET UserLogin='{user.UserLogin}',Email='{user.Email}'"+
                $"                                      WHERE UserID = {user.UserID}");         
            (await context.Users.FirstOrDefaultAsync(x => x.UserID == user.UserID)).Avatar = user.Avatar;     
            
            await context.SaveChangesAsync();
            
        }
        public async Task<IEnumerable<UserRegistration>> GetAllUsers() {
            var users = context.Users.FromSqlRaw("SELECT * FROM Users").ToListAsync();
            return await users;
        }
    }
}
