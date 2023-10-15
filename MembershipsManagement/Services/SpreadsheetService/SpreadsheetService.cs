using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.BazaDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MembershipsManagement.Services.SpreadSheets
{
    public class SpreadsheetService : ISpreadsheetService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SpreadsheetService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetSpreadsheetDto>> CreateBaza(AddSpreadsheetDto baza)
        {
            var response = new ServiceResponse<GetSpreadsheetDto>();
            var fetchedBaza = await _context.Spreadsheets
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

            var bazaToAdd = _mapper.Map<Spreadsheet>(baza);
            _context.Spreadsheets.Add(bazaToAdd);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetSpreadsheetDto>(bazaToAdd);
            response.Message = "Baza has been successfully created";
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteBaza(DeleteSpreadsheetDto baza)
        {
            var response = new ServiceResponse<string>();
            var fetchedBaza = await _context.Spreadsheets
                .Where(b => b.DzematId.Equals(baza.DzematId))
                .Where(b => b.Id == baza.Id)
                .FirstOrDefaultAsync();

            if (fetchedBaza is null)
            {
                throw new BadRequestException("Baza with ID: " + baza.Id + " for džemat ID: " + baza.DzematId + " does not exist.");
            }

            _context.Spreadsheets.Remove(fetchedBaza);
            await _context.SaveChangesAsync();

            response.Data = "Baza with ID: " + baza.Id + " for džemat ID: " + baza.DzematId + " was deleted.";
            response.Message = "Baza has been successfully deleted";
            return response;
        }

        public async Task<ServiceResponse<List<GetSpreadsheetDto>>> GetBazaList(string dzematId)
        {
            var response = new ServiceResponse<List<GetSpreadsheetDto>>();
            var fetched = await _context.Spreadsheets.Where(b => b.DzematId.Equals(dzematId)).ToListAsync();

            if (fetched.Count == 0) {
                response.Message = "No baza available.";
            }

            response.Data = fetched.Select(b => _mapper.Map<GetSpreadsheetDto>(b)).ToList();
            response.Message = "Fetch successful.";
            return response;
        }

        public async Task<ServiceResponse<GetSpreadsheetDto>> GetSingleBaza(int bazaId)
        {
            var response = new ServiceResponse<GetSpreadsheetDto>();
            var fetched = await _context.Spreadsheets.Where(b => b.Id == bazaId).FirstOrDefaultAsync();


            response.Data = _mapper.Map<GetSpreadsheetDto>(fetched);
            response.Message = fetched != null? "Fetch successful." : "No baza was found.";
            return response;
        }

        public async Task<ServiceResponse<GetSpreadsheetDto>> ArchiveBaza(int bazaId)
        {
            var response = new ServiceResponse<GetSpreadsheetDto>();
            var fetched = await _context.Spreadsheets.Where(b => b.Id == bazaId).FirstOrDefaultAsync();
            if(fetched is null)
            {
                throw new EntityNotFoundException("Baza with ID: " + bazaId + " was not found.");
            }
            fetched.Archived = true;
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetSpreadsheetDto>(fetched);
            response.Message = fetched != null ? "Baza with ID: " + bazaId + " was archived." : "No baza was found.";
            return response;
        }

        public async Task<ServiceResponse<GetSpreadsheetDto>> UnarchiveBaza(int bazaId)
        {
            var response = new ServiceResponse<GetSpreadsheetDto>();
            var fetched = await _context.Spreadsheets.Where(b => b.Id == bazaId).FirstOrDefaultAsync();
            if (fetched is null)
            {
                throw new EntityNotFoundException("Baza with ID: " + bazaId + " was not found.");
            }
            fetched.Archived = false;
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetSpreadsheetDto>(fetched);
            response.Message = fetched != null ? "Baza with ID: " + bazaId + " was unarchived." : "No baza was found.";
            return response;
        }

    }
}
