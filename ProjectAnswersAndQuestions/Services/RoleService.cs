using ProjectAnswersAndQuestions.DAL.Interfaces;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;   
        }
        public async Task<int> SetRole()
        {
            return await roleRepository.SetRole();
        }
    }
}
