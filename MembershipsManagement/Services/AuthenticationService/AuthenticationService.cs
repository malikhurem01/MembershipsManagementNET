using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.AuthDTOs;
using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.DTOs.User;
using MembershipsManagement.DTOs.UserDTOs;
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
                    if(fetchedUser.Position != Roles.GlavniImam) {
                        fetchedUser = await _authContext.Supervisors
                          .Where(user => user.Id.Equals(userId))
                          .Include(user => user.Dzemat)
                          .ThenInclude(dzemat => dzemat.Medzlis)
                          .FirstOrDefaultAsync();
                    }
                    if (fetchedUser.Position == Roles.GlavniImam)
                    {
                        fetchedUser = await _authContext.Supervisors
                          .Where(user => user.Id.Equals(userId))
                          .Include(user => user.Medzlis)
                          .FirstOrDefaultAsync();
                    }
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
                    .Where(u => u.Id.Equals(fetchedUser.Id)).FirstOrDefaultAsync();

            if(savedUser.Position == Roles.GlavniImam)
            {
                savedUser = await _authContext.Supervisors
                    .Where(u => u.Id.Equals(fetchedUser.Id))
                    .Where(u => u.Medzlis.UserName.Equals(user.InstitutionUserName))
                    .Include(u => u.Medzlis).FirstOrDefaultAsync();
            }
            else
            {
                savedUser = await _authContext.Supervisors
                    .Where(u => u.Id.Equals(fetchedUser.Id))
                    .Where(u => u.Dzemat.UserName.Equals(user.InstitutionUserName))
                    .Include(u => u.Dzemat).FirstOrDefaultAsync();
            }
            
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
            userMap.DateRegistered = DateTime.UtcNow;
            //Dodati da dva korisnika ne mogu imati isti username
            userMap.UserName = user.FirstName.ToLower() + user.LastName.ToLower() + new Random().Next(9999);
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
            response.Data = _mapper.Map<GetUserDto>(userExists);
            return response;
        }

        public string CreateSupervisorToken(Supervisor user)
        {
            List<Claim> claims = new List<Claim>
             {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
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

        public async Task<ServiceResponse<GetUserDto>> ModifyUser(string token, ModifyUserDto modifiedUser)
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

            if (fetchedUser is not null)
            {
                if (fetchedUser.Id.Equals(modifiedUser.Id))
                {
                    fetchedUser = await _authContext.Supervisors
                    .Where(user => user.Id.Equals(userId))
                    .Include(user => user.Dzemat)
                    .ThenInclude(dzemat => dzemat.Medzlis)
                    .FirstOrDefaultAsync();

                    if(fetchedUser is not null)
                    {
                        if (!string.IsNullOrEmpty(modifiedUser.FirstName))
                        {
                            fetchedUser.FirstName = modifiedUser.FirstName;
                        }

                        if(!string.IsNullOrEmpty(modifiedUser.LastName))
                        {
                            fetchedUser.LastName = modifiedUser.LastName;
                        }

                        if(!string.IsNullOrEmpty(modifiedUser.FathersName))
                        {
                            fetchedUser.FathersName = modifiedUser.FathersName;
                        }

                        if(!string.IsNullOrEmpty(modifiedUser.Email))
                        {
                            fetchedUser.Email = modifiedUser.Email;
                            fetchedUser.NormalizedEmail = modifiedUser.Email.ToUpper();
                        }

                        if (!string.IsNullOrEmpty(modifiedUser.UserName))
                        {
                            fetchedUser.UserName = modifiedUser.UserName;
                            fetchedUser.NormalizedUserName = modifiedUser.UserName.ToUpper();
                        }

                        if(!string.IsNullOrEmpty(modifiedUser.PlaceOfResidence))
                        {
                            fetchedUser.PlaceOfResidence = modifiedUser.PlaceOfResidence;
                        }

                        if(!string.IsNullOrEmpty(modifiedUser.Address))
                        {
                            fetchedUser.Address = modifiedUser.Address;
                        }

                        if(!string.IsNullOrEmpty(modifiedUser.ZipCode))
                        {
                            fetchedUser.ZipCode = modifiedUser.ZipCode;
                        }

                        if(!string.IsNullOrEmpty(modifiedUser.PhoneNumber))
                        {
                            fetchedUser.PhoneNumber = modifiedUser.PhoneNumber;
                        }

                        fetchedUser.DateModified = DateTime.UtcNow;

                        await _authContext.SaveChangesAsync();

                    }
                    else
                    {
                        throw new EntityNotFoundException("Fatal Error. USER NOT FOUND.");
                    }
                }
                else
                {
                    throw new BadRequestException("UNAUTHORIZED.");
                }
            }
            else
            {
                throw new EntityNotFoundException("Fatal Error. USER NOT FOUND.");
            }
            response.Data = _mapper.Map<GetUserDto>(fetchedUser);
            response.Message = "User data modified";
            return response;
        }

        public async Task<bool> CheckUserPassword(string id, string password)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (await _userManager.CheckPasswordAsync(user, password))
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<GetUserDto>> ChangeUserPassword(string token, ChangePasswordDto changePassword)
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

            var user = await _userManager.FindByIdAsync(changePassword.Id);

            if (user.Id.Equals(fetchedUser.Id))
            {
                if (changePassword.NewPassword.Equals(changePassword.ConfirmPassword))
                {
                    if(await _userManager.CheckPasswordAsync(user, changePassword.OldPassword))
                    {
                        if(changePassword.OldPassword.Equals(changePassword.NewPassword))
                        {
                            throw new BadRequestException("Nova lozinka ne može biti ista kao trenutna lozinka");
                        }
                        var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
                        if(result.Succeeded)
                        {
                            response.Message = "Lozinka uspješno promijenjena";
                        }
                        else
                        {
                            throw new BadRequestException("Lozinka ne ispunjava uvjete");
                        }
                    }
                    else
                    {
                        throw new BadRequestException("Unijeli ste neispravu trenutnu lozinku");
                    }
                }
                else
                {
                    throw new BadRequestException("Nova lozinke se ne poklapaju");
                }
            }
            response.Data = _mapper.Map<GetUserDto>(fetchedUser);
            return response;
        }
    }
}
