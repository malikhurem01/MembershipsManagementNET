using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.AuthDTOs;
using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.DTOs.User;
using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using MembershipsManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MembershipsManagement.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly DataContext _authContext;
        private readonly IMapper _mapper;

        public AuthenticationService(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager, 
            IMapper mapper, DataContext authContext
         )
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _mapper = mapper;
            _authContext = authContext;
        }
        public async Task<ServiceResponse<GetUserDto>> GetUserByToken(string token)
        {
            var response = new ServiceResponse<GetUserDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();          

            var fetchedUser = await _authContext.Users
                .Where(user => user.Id.Equals(userId))
                .FirstOrDefaultAsync();

            if(fetchedUser is not null)
            {
                if (fetchedUser.Position != Roles.Admin)
                {
                    fetchedUser = await _authContext.Supervisors
                    .Where(user => user.Id.Equals(userId))
                    .Include(user => user.Dzemat)
                    .ThenInclude(dzemat => dzemat.Medzlis)
                    .FirstOrDefaultAsync();
                }
            }
            else
            {
                throw new EntityNotFoundException("Fatal Error. USER NOT FOUND.");
            }

            response.Data = _mapper.Map<GetUserDto>(fetchedUser);
            response.Message = "User has been successfully fetched.";

            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> LoginSupervisor(UserLoginDto user)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            var fetchedUser = await _userManager.FindByEmailAsync(user.Email);
            var savedUser = await _authContext.Supervisors
                .Where(u => u.Id.Equals(fetchedUser.Id))
                .Where(u => u.DzematId.Equals(user.DzematId))
                .Include(u => u.Dzemat).FirstOrDefaultAsync();
            
            if (fetchedUser is null)
            {
                throw new BadRequestException("Wrong credentials");
            }
            if (fetchedUser is not null && savedUser is null)
            {
                throw new BadRequestException("Fatal error!!! Supervisor from different dzemat tried to log in into another dzemat.");
            }
            else
            {
                var result = await _signInManager.CheckPasswordSignInAsync(fetchedUser, user.Password, false);
                if (!result.Succeeded)
                {
                    throw new BadRequestException("Wrong credentials");
                }
                var getUserDto = _mapper.Map<GetUserDto>(fetchedUser);
                getUserDto.Token = CreateSupervisorToken(savedUser);
                response.Data = getUserDto;
                response.Message = "Supervisor " + user.Email + " has successfully logged in.";
            }
            return response;
        }

        public async Task<ServiceResponse<GetDzematDto>> LoginDzemat(DzematLoginDto user)
        {
            ServiceResponse<GetDzematDto> response = new ServiceResponse<GetDzematDto>();
            var fetchedUser = await _userManager.FindByNameAsync(user.UserName);
            var savedDzemat = _authContext.Dzemati
                .Where(d => d.Id.Equals(fetchedUser.Id))
                .Include(d => d.Medzlis).FirstOrDefaultAsync();
            if (fetchedUser == null)
            {
                throw new BadRequestException("Wrong credentials");
            }
            else
            {
                var result = await _signInManager.CheckPasswordSignInAsync(fetchedUser, user.Password, false);
                if (!result.Succeeded)
                {
                    throw new BadRequestException("Wrong credentials");
                }
                var getUserDto = _mapper.Map<GetDzematDto>(savedDzemat.Result);
                response.Data = getUserDto;
                response.Message = "Džemat " + user.UserName + " has successfully logged in.";
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> LoginAdmin(UserLoginDto user)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            var fetchedUser = await _userManager.FindByEmailAsync(user.Email);
            var savedUser = await _authContext.Admins.Where(user => user.Id.Equals(fetchedUser.Id)).FirstOrDefaultAsync();
            if (fetchedUser == null)
            {
                throw new BadRequestException("Wrong credentials");
            }
            if (savedUser == null)
            {
                throw new BadRequestException("Fatal error!!! Admin logged in but not found in database!!!");
            }
            else
            {
                var result = await _signInManager.CheckPasswordSignInAsync(fetchedUser, user.Password, false);
                if (!result.Succeeded)
                {
                    throw new BadRequestException("Wrong credentials");
                }
                var getUserDto = _mapper.Map<GetUserDto>(fetchedUser);
                getUserDto.Token = CreateAdminToken(savedUser);
                response.Data = getUserDto;
                response.Message = "Admin " + user.Email + " has successfully logged in.";
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> RegisterSupervisor(RegisterUserDto user)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            var userExists = await _userManager.FindByEmailAsync(user.Email);
            if (userExists != null)
                throw new BadRequestException("User already exists");

            var userMap = _mapper.Map<Supervisor>(user);
            //Dodati da dva korisnika ne mogu imati isti username
            userMap.UserName = user.FirstName.ToLower() + user.LastName.ToLower();
            var result = await _userManager.CreateAsync(userMap, user.Password);
            StringBuilder stringBuilder = new StringBuilder();
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    stringBuilder.Append(error.Description);
                }
                throw new BadRequestException(stringBuilder.ToString());
            }

            response = await LoginSupervisor(_mapper.Map<UserLoginDto>(user));
            return response;
        }

        public async Task<ServiceResponse<GetDzematDto>> RegisterDzemat(RegisterDzematDto user)
        {
            ServiceResponse<GetDzematDto> response = new ServiceResponse<GetDzematDto>();
            var userMap = _mapper.Map<Dzemat>(user);
            //Dodati da dva korisnika ne mogu imati isti username
            userMap.UserName = user.UserName;
            var result = await _userManager.CreateAsync(userMap, user.Password);
            StringBuilder stringBuilder = new StringBuilder();
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    stringBuilder.Append(error.Description);
                }
                throw new BadRequestException(stringBuilder.ToString());
            }

            response = await LoginDzemat(_mapper.Map<DzematLoginDto>(user));
           
            return response;
        }

        public string CreateSupervisorToken(Supervisor user)
        {
            List<Claim> claims = new List<Claim>
             {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.UserData, user.Dzemat.Id.ToString())
            };

            return WriteToken(claims);
        }

        public string CreateAdminToken(Admin user)
        {
            List<Claim> claims = new List<Claim>
             {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            return WriteToken(claims);
        }

        private string WriteToken(List<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("JWTSettings:SecurityKey").Value));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(int.Parse(_configuration.GetSection("JWTSettings:ExpiryInMinutes").Value)),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
