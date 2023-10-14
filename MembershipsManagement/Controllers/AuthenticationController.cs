using MembershipsManagement.DTOs.User;
using MembershipsManagement.Entities;
using MembershipsManagement.Services.Authentication;
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

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> RegisterUser(RegisterUserDto user)
        {
            var response = await _authService.RegisterUser(user);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> LoginUser(UserLoginDto user)
        {
            var response = await _authService.Login(user);
            return Ok(response);
        }

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
