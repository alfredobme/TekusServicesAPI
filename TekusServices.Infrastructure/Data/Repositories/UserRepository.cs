using Microsoft.EntityFrameworkCore;
using TekusServices.Domain.Entities;
using TekusServices.Domain.Interfaces;

namespace TekusServices.Infrastructure.Data.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<User> GetUser(string code)
        {
            var usuario = await _dbContext.Users
                .FirstOrDefaultAsync(c => c.Code == code);

            return usuario!;
        }
    }

}
