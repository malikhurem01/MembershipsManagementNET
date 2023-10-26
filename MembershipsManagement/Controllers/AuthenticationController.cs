using MembershipsManagement.DTOs.AuthDTOs;
using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.DTOs.User;
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

        [HttpPost("register/dzemat")]
        public async Task<ActionResult<ServiceResponse<GetDzematDto>>> RegisterDzemat(RegisterDzematDto user)
        {
            var response = await _authService.RegisterDzemat(user);
            return Ok(response);
        }

        [HttpPost("register/supervisor")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> RegisterSupervisor(RegisterUserDto user)
        {
            var response = await _authService.RegisterSupervisor(user);
            return Ok(response);
        }

        [HttpPost("login/dzemat")]
        public async Task<ActionResult<ServiceResponse<GetDzematDto>>> LoginDzemat(DzematLoginDto user)
        {
            var response = await _authService.LoginDzemat(user);
            return Ok(response);
        }

        [HttpPost("login/supervisor")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> LoginSupervisor(UserLoginDto user)
        {
            var response = await _authService.LoginSupervisor(user);
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
