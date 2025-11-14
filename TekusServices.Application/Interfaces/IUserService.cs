using TekusServices.Application.DTO;

namespace TekusServices.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(string codes);
    }
}
