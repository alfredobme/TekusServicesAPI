using TekusServices.Domain.Entities;

namespace TekusServices.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string code);
    }
}
