using MembershipsManagement.DTOs.AuthDTOs;
using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.DTOs.User;
using MembershipsManagement.DTOs.UserDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Models;

namespace MembershipsManagement.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<GetUserDto>> RegisterSupervisor(RegisterUserDto user);
        Task<ServiceResponse<GetUserDto>> GetUserByToken(string token);
        Task<ServiceResponse<GetUserDto>> ModifyUser(string token, ModifyUserDto modifiedUser);
        Task<ServiceResponse<GetUserDto>> ChangeUserPassword(string token, ChangePasswordDto changePassword);
        Task<ServiceResponse<GetUserDto>> LoginSupervisor(UserLoginDto user);
        Task<ServiceResponse<GetUserDto>> LoginAdmin(UserLoginDto user);
        string CreateSupervisorToken(Supervisor user);
        string CreateAdminToken(Admin user);
    }
}
