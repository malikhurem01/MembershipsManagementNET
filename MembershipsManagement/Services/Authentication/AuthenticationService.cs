using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.User;
using MembershipsManagement.Entities;
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

        public AuthenticationService(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager, IMapper mapper, DataContext authContext)
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

            var fetchedUser = await _authContext.Korisnici
                .Where(user => user.Id.Equals(userId))
                .Include(user => user.Dzemat)
                .FirstOrDefaultAsync();

            if(fetchedUser is null)
            {
                throw new EntityNotFoundException("Fatal Error. USER NOT FOUND.");
            }

            response.Data = _mapper.Map<GetUserDto>(fetchedUser);
            response.Message = "User has been successfully fetched.";

            return response;

        }

        public async Task<ServiceResponse<GetUserDto>> Login(UserLoginDto user)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            var fetchedUser = await _userManager.FindByEmailAsync(user.Email);
            var savedUser = await _authContext.Korisnici.Where(user => user.Id.Equals(fetchedUser.Id)).Include(user => user.Dzemat).FirstOrDefaultAsync();
            if (fetchedUser == null)
            {
                throw new BadRequestException("Wrong credentials");
            }
            if (savedUser == null)
            {
                throw new BadRequestException("Fatal error. User logged in but not found in database");
            }
            else
            {
                var result = await _signInManager.CheckPasswordSignInAsync(fetchedUser, user.Password, false);
                if (!result.Succeeded)
                {
                    throw new BadRequestException("Wrong credentials");
                }
                var getUserDto = _mapper.Map<GetUserDto>(fetchedUser);
                getUserDto.Token = CreateToken(savedUser);
                response.Data = getUserDto;
                response.Message = "User " + user.Email + " has successfully logged in.";
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> RegisterUser(RegisterUserDto user)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            var userMap = _mapper.Map<Korisnik>(user);
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
            response = await Login(_mapper.Map<UserLoginDto>(user));
            return response;

        }

        public string CreateToken(Korisnik user)
        {
            List<Claim> claims;
            if(user.DzematId is not null)
            {
                claims = new List<Claim>
             {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.UserData, user.Dzemat.Id.ToString())
            };
            }
            else
            {
                claims = new List<Claim>
             {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.UserData, "all")
            };
            }
            

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
