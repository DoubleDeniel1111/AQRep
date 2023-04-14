using ProjectAnswersAndQuestions.DAL;
using ProjectAnswersAndQuestions.DAL.Interfaces;
using ProjectAnswersAndQuestions.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddUser(UserRegistration user)
        {
           await userRepository.AddUser(user);
        }
        public async Task<UserRegistration> GetUserByData(UserLogin user)
        {
            UserRegistration _user = await userRepository.GetUserByData(user);
            return _user; 
        }
        public async Task<UserRegistration> GetUserByData(UserRegistration user)
        {
            UserRegistration _user = await userRepository.GetUserByData(user);
            return _user;
        }
        public async Task<UserRegistration> GetUserByEmail(string email)
        {
            var _user = await userRepository.GetUserByEmail(email);
            return _user;
        }
        public async Task<UserRegistration> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }
        public async Task UpdateUser(UserRegistration user)
        {
            await userRepository.UpdateUser(user);
        }
        public async Task<IEnumerable<UserRegistration>> GetAllUsers()
        {
            return await userRepository.GetAllUsers();
        }
        public  ClaimsIdentity Authenticate(UserLogin user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,"");
        }
    }
}
