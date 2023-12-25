using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.AuthDTOs;
using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.DTOs.MedzlisDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using Microsoft.EntityFrameworkCore;

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
