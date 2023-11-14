using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.BazaDTOs;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.SpreadsheetDTOs;
using MembershipsManagement.DTOs.User;
using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public async Task<ServiceResponse<GetSpreadsheetDto>> CreateBaza(string token, AddSpreadsheetDto baza)
        {
            var response = new ServiceResponse<GetSpreadsheetDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var fetchedBaza = await _context.Spreadsheets
                .Where(b => b.DzematId
                .Equals(baza.DzematId))
                .Where(b => b.Year == baza.Year)
                .FirstOrDefaultAsync();

            var activeBaza = await _context.Spreadsheets
                .Where(b => b.DzematId
                .Equals(baza.DzematId))
                .Where(b => b.Archived == false)
                .FirstOrDefaultAsync();

            var member = await _context.Supervisors
                .Where(s => s.Id
                .Equals(userId))
                .Where(s => s.DzematId
                .Equals(baza.DzematId))
                .FirstOrDefaultAsync();

            if (member is null)
            {
                throw new BadRequestException("FATAL ERROR. Supervisor which is not a part of Dzemat tried to create a new spreadsheet.");
            }

            if(activeBaza is not null)
            {
                throw new BadRequestException("Arhivirajte sve prethodne baze");
            }

            if(fetchedBaza is not null)
            {
                throw new BadRequestException("Baza za godinu " + baza.Year + " već postoji");
            }

            if (baza.Year > DateTime.Now.Year)
            {
                throw new BadRequestException("Godina mora biti manja od " + (DateTime.Now.Year + 1) + ".");
            }

            var bazaToAdd = _mapper.Map<Spreadsheet>(baza);

            var members = await _context.Members
                .Where(m => m.DzematId
                .Equals(bazaToAdd.DzematId))
                .Where(m => m.Active == true)
                .ToListAsync();

            bazaToAdd.Members.AddRange(members);

            members.ForEach(m => {
                int debtToAdd;
                switch (m.Status)
                {
                    case Status.Brak:
                        debtToAdd = bazaToAdd.MarriageFee;
                        break;
                    case Status.DobnaGranica:
                        debtToAdd = bazaToAdd.AgeLimitFee;
                        break;
                    case Status.Udovac:
                        debtToAdd = bazaToAdd.WidowerFee;
                        break;
                    default:
                        debtToAdd = 0;
                        break;
                }
                m.Debt += debtToAdd;
            });

            _context.Spreadsheets.Add(bazaToAdd);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetSpreadsheetDto>(bazaToAdd);
            response.Message = "Baza uspješno kreirana";
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteBaza(string token, DeleteSpreadsheetDto baza)
        {
            var response = new ServiceResponse<string>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
               .Where(claim => claim.Type
               .Equals("nameid"))
               .Select(claim => claim.Value)
               .SingleOrDefault();
            var member = await _context.Supervisors.Where(s => s.Id.Equals(userId)).Where(s => s.DzematId.Equals(baza.DzematId)).FirstOrDefaultAsync();
            if (member is null)
            {
                throw new BadRequestException("FATAL ERROR. Supervisor which is not a part of Dzemat tried to delete a spreadsheet.");

            }
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
            var fetched = await _context.Spreadsheets.Where(b => b.DzematId.Equals(dzematId)).OrderByDescending(b => b.Year).ToListAsync();

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

        public async Task<ServiceResponse<GetSpreadsheetDto>> ArchiveBaza(string token, ArchiveSpreadsheetDto spreadsheet)
        {
            var response = new ServiceResponse<GetSpreadsheetDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
               .Where(claim => claim.Type
               .Equals("nameid"))
               .Select(claim => claim.Value)
               .SingleOrDefault();

            var fetchedSpreadsheet = await _context.Spreadsheets
                .Where(b => b.Id == spreadsheet.SpreadsheetId)
                .Where(s => s.DzematId.Equals(spreadsheet.DzematId))
                .FirstOrDefaultAsync();

            if(fetchedSpreadsheet is null)
            {
                throw new EntityNotFoundException("Baza with ID: " + spreadsheet.SpreadsheetId + " was not found.");
            }
            fetchedSpreadsheet.Archived = true;
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetSpreadsheetDto>(fetchedSpreadsheet);
            response.Message = fetchedSpreadsheet != null ? "Baza za godinu " + fetchedSpreadsheet.Year + " je uspješno arhivirana" : "No spreadsheet was found.";
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

        public async Task<ServiceResponse<object>> GetActiveSpreadsheet(string token)
        {
            var response = new ServiceResponse<object>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
               .Where(claim => claim.Type
               .Equals("nameid"))
               .Select(claim => claim.Value)
               .SingleOrDefault();

            var activeSpreadsheet = await _context.Supervisors
                .Where(s => s.Id.Equals(userId)).Select(s => new { 
                 spreadsheet = s.Dzemat.Spreadsheets
                 .Where(s => s.Archived == false)
                 .Select(s => new 
                 {
                     id = s.Id, 
                     year = s.Year
                 })
                 .First()
                }).FirstOrDefaultAsync();

            if(activeSpreadsheet is null)
            {
                throw new EntityNotFoundException("Aktivna baza ne postoji. Molimo Vas da je kreirate");
            }

            _context.ChangeTracker.LazyLoadingEnabled = false;

            var rawMembersInfo = await _context.MembersInfo
                .Where(mi => mi.SpreadsheetId == activeSpreadsheet.spreadsheet.id)
                .Include(mi => mi.Spreadsheet)
                .Include(mi => mi.Member)
                .ThenInclude(m => m.FamilyMembers)
                .Include(mi => mi.Payments)
                .ThenInclude(p => p.Supervisor)
                .Select(mi => new
                {
                    totalAmountPayed = mi.TotalAmountPayed,
                    member = _mapper.Map<GetMemberDto>(mi.Member),
                    note = mi.Note,
                    membershipFee = mi.MembershipFee,
                    latestPayment = mi.Payments.OrderByDescending(p => p.DateOfPayment).FirstOrDefault(),
                    payments = mi.Payments.Select(y => new
                    {
                        id = y.Id,
                        amount = y.Amount,
                        supervisor = _mapper.Map<GetUserDto>(y.Supervisor),
                        dateOfPayment = y.DateOfPayment
                    }).ToArray()
                }).ToListAsync();

            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            response.Data = JsonSerializer.Serialize(new { spreadsheet = activeSpreadsheet.spreadsheet, rawMembersInfo}, options);
            response.Message = "Aktivna baza uspješno učitana";
            return response;
        }
    }
}
