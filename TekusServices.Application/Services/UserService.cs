using AutoMapper;
using TekusServices.Application.DTO;
using TekusServices.Application.Interfaces;
using TekusServices.Domain.Interfaces;

namespace TekusServices.Application.Services
{
    public class UserService(IMapper mapper, IUserRepository userRepository) : IUserService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserDTO> GetUser(string code)
        {
            var user = await _userRepository.GetUser(code);

            return _mapper.Map<UserDTO>(user);
        }
    }
}
