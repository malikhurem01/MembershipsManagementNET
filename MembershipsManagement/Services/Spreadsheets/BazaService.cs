using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.BazaDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MembershipsManagement.Services.SpreadSheets
{
    public class BazaService : IBazaService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BazaService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetBazaDto>> CreateBaza(AddBazaDto baza)
        {
            var response = new ServiceResponse<GetBazaDto>();
            var fetchedBaza = await _context.SpreadSheets
                .Where(b => b.DzematId.Equals(baza.DzematId))
                .Where(b => b.Year == baza.Year)
                .FirstOrDefaultAsync();

            if(fetchedBaza is not null)
            {
                throw new BadRequestException("Baza for year " 
                    + baza.Year + " for Džemat ID: " 
                    + baza.DzematId 
                    + " already exists.");
            }

            if (baza.Year > DateTime.Now.Year)
            {
                throw new BadRequestException("Year can not be greater than: " + DateTime.Now.Year);
            }

            var bazaToAdd = _mapper.Map<SpreadSheet>(baza);
            _context.SpreadSheets.Add(bazaToAdd);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetBazaDto>(bazaToAdd);
            response.Message = "Baza has been successfully created";
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteBaza(DeleteBazaDto baza)
        {
            var response = new ServiceResponse<string>();
            var fetchedBaza = await _context.SpreadSheets
                .Where(b => b.DzematId.Equals(baza.DzematId))
                .Where(b => b.Id == baza.Id)
                .FirstOrDefaultAsync();

            if (fetchedBaza is null)
            {
                throw new BadRequestException("Baza with ID: " + baza.Id + " for džemat ID: " + baza.DzematId + " does not exist.");
            }

            _context.SpreadSheets.Remove(fetchedBaza);
            await _context.SaveChangesAsync();

            response.Data = "Baza with ID: " + baza.Id + " for džemat ID: " + baza.DzematId + " was deleted.";
            response.Message = "Baza has been successfully deleted";
            return response;
        }

        public async Task<ServiceResponse<List<GetBazaDto>>> GetBazaList(int dzematId)
        {
            var response = new ServiceResponse<List<GetBazaDto>>();
            var fetched = await _context.SpreadSheets.Where(b => b.DzematId.Equals(dzematId)).ToListAsync();

            if (fetched.Count == 0) {
                response.Message = "No baza available.";
            }

            response.Data = fetched.Select(b => _mapper.Map<GetBazaDto>(b)).ToList();
            response.Message = "Fetch successful.";
            return response;
        }

        public async Task<ServiceResponse<GetBazaDto>> GetSingleBaza(int bazaId)
        {
            var response = new ServiceResponse<GetBazaDto>();
            var fetched = await _context.SpreadSheets.Where(b => b.Id == bazaId).FirstOrDefaultAsync();


            response.Data = _mapper.Map<GetBazaDto>(fetched);
            response.Message = fetched != null? "Fetch successful." : "No baza was found.";
            return response;
        }

        public async Task<ServiceResponse<GetBazaDto>> ArchiveBaza(int bazaId)
        {
            var response = new ServiceResponse<GetBazaDto>();
            var fetched = await _context.SpreadSheets.Where(b => b.Id == bazaId).FirstOrDefaultAsync();
            if(fetched is null)
            {
                throw new EntityNotFoundException("Baza with ID: " + bazaId + " was not found.");
            }
            fetched.Archived = true;
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetBazaDto>(fetched);
            response.Message = fetched != null ? "Baza with ID: " + bazaId + " was archived." : "No baza was found.";
            return response;
        }

        public async Task<ServiceResponse<GetBazaDto>> UnarchiveBaza(int bazaId)
        {
            var response = new ServiceResponse<GetBazaDto>();
            var fetched = await _context.SpreadSheets.Where(b => b.Id == bazaId).FirstOrDefaultAsync();
            if (fetched is null)
            {
                throw new EntityNotFoundException("Baza with ID: " + bazaId + " was not found.");
            }
            fetched.Archived = false;
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetBazaDto>(fetched);
            response.Message = fetched != null ? "Baza with ID: " + bazaId + " was unarchived." : "No baza was found.";
            return response;
        }

    }
}
