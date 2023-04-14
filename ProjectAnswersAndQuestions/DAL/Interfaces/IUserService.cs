using ProjectAnswersAndQuestions.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.DAL.Interfaces
{
    public interface IUserService
    {
        Task AddUser(UserRegistration entity);
        Task<UserRegistration> GetUserByData(UserLogin entity);
        Task<UserRegistration> GetUserByData(UserRegistration entity);
        Task<UserRegistration> GetUserByEmail(string email);
        Task<UserRegistration> GetUserById(int id);
        Task<IEnumerable<UserRegistration>> GetAllUsers();
        Task UpdateUser(UserRegistration entity);
        ClaimsIdentity Authenticate(UserLogin entity);
    }
}
