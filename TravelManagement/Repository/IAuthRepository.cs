using TravelManagement.Models.Dto;
using TravelManagement.Models;

namespace TravelManagement.Repository
{
    public interface IAuthRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto);
        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDto);
    }
}
