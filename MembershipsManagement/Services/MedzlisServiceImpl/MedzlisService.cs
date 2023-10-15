using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.MedzlisDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MembershipsManagement.Services.MedzlisServiceImpl
{
    public class MedzlisService : IMedzlisService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MedzlisService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetMedzlisDto>> CreateMedzlis(AddMedzlisDto medzlis)
        {
            var response = new ServiceResponse<GetMedzlisDto>();
            var fetchedMedzlis = await _context.Medzlisi.Where(m => m.Name.Equals(medzlis.Name)).FirstOrDefaultAsync();

            if (fetchedMedzlis is not null)
                throw new BadRequestException("Medzlis " + medzlis.Name + " is already registered in database.");

            Medzlis newMedzlis = _mapper.Map<Medzlis>(medzlis);

            _context.Medzlisi.Add(newMedzlis);

            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetMedzlisDto>(newMedzlis);
            response.Message = "Medzlis " + newMedzlis.Name + " has successfully been created.";

            return response;
        }

        public async Task<ServiceResponse<string>> DeleteMedzlis(int medzlisId)
        {
            var response = new ServiceResponse<string>();
            var fetchedMedzlis = await _context.Medzlisi.Where(m => m.Id == medzlisId).FirstOrDefaultAsync();

            if (fetchedMedzlis is null)
                throw new EntityNotFoundException("Medzlis ID: " + medzlisId + " is not registed in the database.");

            _context.Medzlisi.Remove(fetchedMedzlis);
            await _context.SaveChangesAsync();

            response.Data = "";
            response.Message = "Medzlis ID: " + fetchedMedzlis.Id + " was successfully deleted from the database.";

            return response;
        }

        public async Task<ServiceResponse<GetMedzlisDto>> GetMedzlis(int medzlisId)
        {
            var response = new ServiceResponse<GetMedzlisDto>();
            var fetchedMedzlis = await _context.Medzlisi.Where(m => m.Id == medzlisId).FirstOrDefaultAsync();

            if (fetchedMedzlis is null)
                throw new EntityNotFoundException("Medzlis ID: " + medzlisId + " is not registed in the database.");

            response.Data = _mapper.Map<GetMedzlisDto>(fetchedMedzlis);
            response.Message = "Medzlis ID: " + fetchedMedzlis.Id + " was succesfully fetched from the database.";

            return response;
        }

        public async Task<ServiceResponse<List<GetMedzlisDto>>> GetMedzlisList()
        {
            var response = new ServiceResponse<List<GetMedzlisDto>>();
            var medzlisList = await _context.Medzlisi.ToListAsync();

            response.Data = medzlisList.Select(m => _mapper.Map<GetMedzlisDto>(m)).ToList();
            response.Message = "List of all medzlis has been fetched from the database.";

            return response;
        }

        public async Task<ServiceResponse<GetMedzlisDto>> ModifyMedzlis(ModifyMedzlisDto medzlis)
        {
            var response = new ServiceResponse<GetMedzlisDto>();
            var fetchedMedzlis = await _context.Medzlisi.Where(m => m.Id == medzlis.Id).FirstOrDefaultAsync();

            if (fetchedMedzlis is null)
                throw new EntityNotFoundException("Medzlis ID: " + medzlis.Id + " is not registed in the database.");

            fetchedMedzlis.Name = medzlis.Name.Length > 0 ? medzlis.Name : fetchedMedzlis.Name;
            fetchedMedzlis.Location = medzlis.Location.Length > 0 ? medzlis.Location : fetchedMedzlis.Location;
            fetchedMedzlis.Address = medzlis.Address.Length > 0 ? medzlis.Address : fetchedMedzlis.Address;
            fetchedMedzlis.ZipCode = medzlis.ZipCode > 0 ? (int)medzlis.ZipCode : fetchedMedzlis.ZipCode;

            response.Data = _mapper.Map<GetMedzlisDto>(fetchedMedzlis);
            response.Message = "Medzlis ID: " + fetchedMedzlis.Id + " was succesfully modified.";

            return response;
        }
    }
}
