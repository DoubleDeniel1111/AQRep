using Microsoft.EntityFrameworkCore;
using ProjectAnswersAndQuestions.DAL.Interfaces;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.DAL
{
    public class RoleRepository : IRoleRepository
    {
        
        private readonly AppDbContext context;
        public RoleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> SetRole()
        {
           // return (await context.Roles.FirstOrDefaultAsync(x => x.UserRole == "USER")).RoleID;
              return (await context.Roles.FromSqlRaw
                ($"SELECT * FROM Roles")
                .FirstOrDefaultAsync(x => x.UserRole == "User"))
                .RoleID;
        }

    }
}

