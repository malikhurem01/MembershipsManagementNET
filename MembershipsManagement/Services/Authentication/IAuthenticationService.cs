using MembershipsManagement.DTOs.User;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<GetUserDto>> RegisterUser(RegisterUserDto user);
        Task<ServiceResponse<GetUserDto>> GetUserByToken(string token);
        Task<ServiceResponse<GetUserDto>> Login(UserLoginDto user);
        string CreateToken(Korisnik user);
    }
}
