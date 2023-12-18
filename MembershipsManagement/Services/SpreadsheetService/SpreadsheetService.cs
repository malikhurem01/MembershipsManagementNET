using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.SpreadsheetDTOs;
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

        public async Task<ServiceResponse<GetSpreadsheetDto>> CreateSpreadsheet(string token, AddSpreadsheetDto spreadsheet)
        {
            var response = new ServiceResponse<GetSpreadsheetDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var fetchedSpreadsheet = await _context.Spreadsheets
                .Where(b => b.DzematId == spreadsheet.DzematId)
                .Where(b => b.Year == spreadsheet.Year)
                .FirstOrDefaultAsync();

            var activeSpreadsheet = await _context.Spreadsheets
                .Where(b => b.DzematId == spreadsheet.DzematId)
                .Where(b => b.Archived == false)
                .FirstOrDefaultAsync();

            var member = await _context.Supervisors
                .Where(s => s.Id
                .Equals(userId))
                .Where(s => s.DzematId == spreadsheet.DzematId)
                .FirstOrDefaultAsync();

            if (member is null)
            {
                throw new BadRequestException("FATAL ERROR. Supervisor which is not a part of Dzemat tried to create a new spreadsheet.");
            }

            if(activeSpreadsheet is not null)
            {
                throw new BadRequestException("Arhivirajte sve prethodne baze");
            }

            if(fetchedSpreadsheet is not null)
            {
                throw new BadRequestException("Spreadsheet za godinu " + spreadsheet.Year + " već postoji");
            }

            if (spreadsheet.Year > DateTime.Now.Year)
            {
                throw new BadRequestException("Godina mora biti manja od " + (DateTime.Now.Year + 1) + ".");
            }

            var spreadsheetToAdd = _mapper.Map<Spreadsheet>(spreadsheet);

            var members = await _context.Members
                .Where(m => m.DzematId == spreadsheetToAdd.DzematId)
                .Where(m => m.Active == true)
                .ToListAsync();

            spreadsheetToAdd.Members.AddRange(members);

            members.ForEach(m => {
                int debtToAdd;
                switch (m.Status)
                {
                    case Status.Brak:
                        debtToAdd = spreadsheetToAdd.MarriageFee;
                        break;
                    case Status.DobnaGranica:
                        debtToAdd = spreadsheetToAdd.AgeLimitFee;
                        break;
                    case Status.Udovac:
                        debtToAdd = spreadsheetToAdd.WidowerFee;
                        break;
                    default:
                        debtToAdd = 0;
                        break;
                }
                m.Debt += debtToAdd;
            });

            _context.Spreadsheets.Add(spreadsheetToAdd);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetSpreadsheetDto>(spreadsheetToAdd);
            response.Message = "Spreadsheet uspješno kreirana";
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteSpreadsheet(string token, DeleteSpreadsheetDto spreadsheet)
        {
            var response = new ServiceResponse<string>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
               .Where(claim => claim.Type
               .Equals("nameid"))
               .Select(claim => claim.Value)
               .SingleOrDefault();
            var member = await _context.Supervisors.Where(s => s.Id.Equals(userId)).Where(s => s.DzematId == spreadsheet.DzematId).FirstOrDefaultAsync();
            if (member is null)
            {
                throw new BadRequestException("FATAL ERROR. Supervisor which is not a part of Dzemat tried to delete a spreadsheet.");

            }
            var fetchedSpreadsheet = await _context.Spreadsheets
                .Where(b => b.DzematId == spreadsheet.DzematId)
                .Where(b => b.Id == spreadsheet.Id)
                .FirstOrDefaultAsync();

            if (fetchedSpreadsheet is null)
            {
                throw new BadRequestException("Spreadsheet with ID: " + spreadsheet.Id + " for džemat ID: " + spreadsheet.DzematId + " does not exist.");
            }

            _context.Spreadsheets.Remove(fetchedSpreadsheet);
            await _context.SaveChangesAsync();

            response.Data = "Spreadsheet with ID: " + spreadsheet.Id + " for džemat ID: " + spreadsheet.DzematId + " was deleted.";
            response.Message = "Spreadsheet has been successfully deleted";
            return response;
        }

        public async Task<ServiceResponse<List<GetSpreadsheetDto>>> GetSpreadsheetList(int dzematId)
        {
            var response = new ServiceResponse<List<GetSpreadsheetDto>>();
            var fetched = await _context.Spreadsheets.Where(b => b.DzematId == dzematId).OrderByDescending(b => b.Year).ToListAsync();

            if (fetched.Count == 0) {
                response.Message = "No spreadsheet available.";
            }

            response.Data = fetched.Select(b => _mapper.Map<GetSpreadsheetDto>(b)).ToList();
            response.Message = "Fetch successful.";
            return response;
        }

        public async Task<ServiceResponse<GetSpreadsheetDto>> GetSingleSpreadsheet(int spreadsheetId)
        {
            var response = new ServiceResponse<GetSpreadsheetDto>();
            var fetched = await _context.Spreadsheets.Where(b => b.Id == spreadsheetId).FirstOrDefaultAsync();
            response.Data = _mapper.Map<GetSpreadsheetDto>(fetched);
            response.Message = fetched != null? "Fetch successful." : "No spreadsheet was found.";
            return response;
        }

        public async Task<ServiceResponse<GetSpreadsheetDto>> ArchiveSpreadsheet(string token, ArchiveSpreadsheetDto spreadsheet)
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
                .Where(s => s.DzematId == spreadsheet.DzematId)
                .FirstOrDefaultAsync();

            if(fetchedSpreadsheet is null)
            {
                throw new EntityNotFoundException("Spreadsheet with ID: " + spreadsheet.SpreadsheetId + " was not found.");
            }
            fetchedSpreadsheet.Archived = true;
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetSpreadsheetDto>(fetchedSpreadsheet);
            response.Message = fetchedSpreadsheet != null ? "Spreadsheet za godinu " + fetchedSpreadsheet.Year + " je uspješno arhivirana" : "No spreadsheet was found.";
            return response;
        }

        public async Task<ServiceResponse<GetSpreadsheetDto>> UnarchiveSpreadsheet(int spreadsheetId)
        {
            var response = new ServiceResponse<GetSpreadsheetDto>();
            var fetched = await _context.Spreadsheets.Where(b => b.Id == spreadsheetId).FirstOrDefaultAsync();
            if (fetched is null)
            {
                throw new EntityNotFoundException("Spreadsheet with ID: " + spreadsheetId + " was not found.");
            }
            fetched.Archived = false;
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetSpreadsheetDto>(fetched);
            response.Message = fetched != null ? "Spreadsheet with ID: " + spreadsheetId + " was unarchived." : "No spreadsheet was found.";
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

            if (activeSpreadsheet is null)
            {
                throw new EntityNotFoundException("Aktivna spreadsheet ne postoji. Molimo Vas da je kreirate");
            }

            _context.ChangeTracker.LazyLoadingEnabled = false;

            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            response.Data = JsonSerializer.Serialize(new { spreadsheet = activeSpreadsheet.spreadsheet }, options);
            response.Message = "Aktivna spreadsheet uspješno učitana";
            return response;
        }

        public async Task<ServiceResponse<SpreadsheetReportDto>> GetSpreadsheetReport(string token, int spreadsheetYear)
        {
            var response = new ServiceResponse<SpreadsheetReportDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
               .Where(claim => claim.Type
               .Equals("nameid"))
               .Select(claim => claim.Value)
               .SingleOrDefault();

            var spreadsheet = await _context.Supervisors
                .Include(s => s.Dzemat)
                .ThenInclude(d => d.Spreadsheets)
                .Where(s => s.Id.Equals(userId))
                .Select(s => s.Dzemat.Spreadsheets.Where(spreadsheet => spreadsheet.Year == spreadsheetYear)
                .Select(spreadsheet => spreadsheet.Id).ToList()).ToListAsync();

            if(spreadsheet.ElementAt(0).Count == 0)
            {
                throw new BadRequestException("Spreadsheet za godinu " + spreadsheetYear + " ne postoji");
            }

            var spreadsheetId = spreadsheet.ElementAt(0).ElementAt(0);

            var spreadsheetReport = await _context.Supervisors
                            .Where(s => s.Id.Equals(userId))
                            .Select(sr => new SpreadsheetReportDto
                            {
                                MedzlisName = sr.Dzemat.Medzlis.Name,
                                DzematName = sr.Dzemat.Name,
                                MarriageStatusMembersCount = sr.Dzemat.Spreadsheets
                                .Where(spreadsheet => spreadsheet.Id == spreadsheetId)
                                .Select(activeSpreadsheet => activeSpreadsheet.Members.Where(member => member.Status == Status.Brak).Count()).First(),
                                AgeLimitStatusMembersCount = sr.Dzemat.Spreadsheets
                                .Where(spreadsheet => spreadsheet.Id == spreadsheetId)
                                .Select(activeSpreadsheet => activeSpreadsheet.Members.Where(member => member.Status == Status.DobnaGranica).Count()).First(),
                                WidowerStatusMembersCount = sr.Dzemat.Spreadsheets
                                .Where(spreadsheet => spreadsheet.Id == spreadsheetId)
                                .Select(activeSpreadsheet => activeSpreadsheet.Members.Where(member => member.Status == Status.Udovac).Count()).First(),
                                Budget = (double)sr.Dzemat.Budget,
                                TotalPaymentsAmountListed = sr.Dzemat.Spreadsheets
                                .Where(spreadsheet => spreadsheet.Id == spreadsheetId)
                                .Select(spreadsheet => spreadsheet.Payments.GroupBy(payments => payments.SpreadsheetId).Select(p => p.Sum(p => p.Amount)).ToList()).ToList(),
                                MembersPayed = sr.Dzemat.Spreadsheets
                                .Where(spreadsheet => spreadsheet.Id == spreadsheetId)
                                .SelectMany(spreadsheet => spreadsheet.Members
                                .Where(member => member.Debt == 0)
                                .Select(member => new MemberThatHasPayedDto
                                {
                                    FullName = member.FirstName + " " + member.LastName,
                                    Supervisor = "DORADITI",
                                    TotalAmount = member.Payments.Where(payment => payment.SpreadsheetId == spreadsheetId).Sum(p => p.Amount)
                                })).ToList(),
                                MembersNotPayed = sr.Dzemat.Spreadsheets
                                .Where(spreadsheet => spreadsheet.Id == spreadsheetId)
                                .SelectMany(spreadsheet => spreadsheet.Members
                                .Where(member => member.Debt > 0)
                                .Select(member => new MemberThatHasNotPayedDto
                                {
                                    FullName = member.FirstName + " " + member.LastName,
                                    Debt = member.Debt,
                                    TotalAmount = member.Payments.Where(payment => payment.SpreadsheetId == spreadsheetId).Sum(p => p.Amount)
                                })).ToList(),

                            }).FirstOrDefaultAsync();

            response.Data = spreadsheetReport;
            response.Message = "Izvještaj je uspješno izrađen";
            return response;
        }

        public async Task<ServiceResponse<List<DebtWarningReport>>> GetDebtWarningReports(string token)
        {
            var response = new ServiceResponse<List<DebtWarningReport>>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
               .Where(claim => claim.Type
               .Equals("nameid"))
               .Select(claim => claim.Value)
               .SingleOrDefault();

            var debtWarningReports = await _context.Supervisors
                .Where(s => s.Id.Equals(userId))
                .SelectMany(s => s.Dzemat.Members.Where(member => member.Debt > 0).Select(member => new DebtWarningReport
                {
                    DzematName = member.Dzemat.Name,
                    Debt = member.Debt,
                    FirstName = member.FirstName,
                    FathersName = member.FathersName,
                    LastName = member.LastName
                })).ToListAsync();

            if(debtWarningReports.Count == 0)
            {
                throw new BadRequestException("Nema članova sa dugom");
            }

            response.Data = debtWarningReports;
            response.Message = "Opomene uspješno izrađene";
            return response;
        }
    }
}
