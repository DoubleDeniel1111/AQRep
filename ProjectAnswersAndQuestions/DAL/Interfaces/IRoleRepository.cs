using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.DAL.Interfaces
{
    public interface IRoleRepository
    {
        Task<int> SetRole();
    }
}
