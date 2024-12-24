using MembershipsManagement.DTOs.AuthDTOs;
using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.DTOs.User;
using MembershipsManagement.DTOs.UserDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembershipsManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("register/supervisor")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> RegisterSupervisor(RegisterUserDto user)
        {
            var response = await _authService.RegisterSupervisor(user);
            return Ok(response);
        }

        [Authorize]
        [HttpPatch("user/modify")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> ModifyUser(ModifyUserDto modifiedUser)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _authService.ModifyUser(tokenValue, modifiedUser);
            return Ok(response);
        }

        [Authorize]
        [HttpPatch("user/password")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> ChangeUserPassword(ChangePasswordDto changePassword)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _authService.ChangeUserPassword(tokenValue, changePassword);
            return Ok(response);
        }

        [HttpPost("login/supervisor")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> LoginSupervisor(UserLoginDto user)
        {
            var response = await _authService.LoginSupervisor(user);
            return Ok(response);
        }

        [HttpPost("login/member")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> LoginMember(MemberLoginDto user)
        {
            var response = await _authService.LoginMember(user);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("current/user")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUserByToken()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _authService.GetUserByToken(tokenValue);
            return Ok(response);
        }
    }
}
