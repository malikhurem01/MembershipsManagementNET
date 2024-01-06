using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.AuthDTOs;
using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.DTOs.MedzlisDTOs;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace MembershipsManagement.Services.DzematServiceImpl
{
    public class DzematService : IDzematService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DzematService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetDzematDto>>> GetMedzlisDzemat(string token, int medzlisId)
        {
            var response = new ServiceResponse<List<GetDzematDto>>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var supervisor = await _context.Supervisors.Where(s => s.Id.Equals(userId)).Where(s => s.MedzlisId == medzlisId).FirstOrDefaultAsync();
            
            if(supervisor is null)
            {
                throw new UnauthorizedAccessException("Nemate ovlaštenje za ovu radnju.");
            }

            var listDzemat = await _context.Dzemati.Where(dz => dz.MedzlisId == medzlisId).OrderBy(dz => dz.Name).ToListAsync();

            response.Data = listDzemat.Select(dz => _mapper.Map<GetDzematDto>(dz)).ToList();
            response.Message = "Džemati učitani";
            return response;
        }

        public async Task<ServiceResponse<GetDzematDto>> RegisterDzemat(RegisterDzematDto registerDzemat)
        {
            var response = new ServiceResponse<GetDzematDto>();
            var fetchedDzemat = await _context.Dzemati.Where(m => m.Name.Equals(registerDzemat.Name)).FirstOrDefaultAsync();

            if (fetchedDzemat is not null)
                throw new BadRequestException("Dzemat " + fetchedDzemat.Name + " is already registered in database.");

            Dzemat newDzemat = _mapper.Map<Dzemat>(registerDzemat);

            newDzemat.DateRegistered = DateTime.UtcNow;

            _context.Dzemati.Add(newDzemat);

            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetDzematDto>(newDzemat);
            response.Message = "Medzlis " + newDzemat.Name + " has successfully been created.";

            return response;
        }
    }
}
