using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.ProjectDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using MembershipsManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace MembershipsManagement.Services.ProjectServiceImpl
{
    public class IncomeItemService : IIncomeItemService
    {

        private readonly DataContext _incomeItemContext;
        private readonly IMapper _mapper;

        public IncomeItemService(DataContext incomeItemContext, IMapper mapper)
        {
            _incomeItemContext = incomeItemContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetIncomeDto>> AddIncome(string token, AddIncomeDto income)
        {
            var response = new ServiceResponse<GetIncomeDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _incomeItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .Where(s => s.DzematId == income.DzematId)
                .FirstOrDefaultAsync();

            if (supervisor == null)
            {
                throw new UnauthorizedAccessException("Not authorized.");
            }

            var incomeItem = await _incomeItemContext.IncomeItems.Where(p => p.Id == income.IncomeItemId).Include(p => p.Spreadsheet).FirstOrDefaultAsync();

            var incomeToAdd = _mapper.Map<Income>(income);
            incomeToAdd.Supervisor = supervisor;
            incomeItem.Income.Add(incomeToAdd);

            await _incomeItemContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetIncomeDto>(incomeToAdd);
            response.Message = "Priliv novca je upsješno zaveden";

            return response;
        }

        public async Task<ServiceResponse<GetIncomeItemDto>> CreateIncomeItem(string token, CreateIncomeItem incomeItem)
        {
            var response = new ServiceResponse<GetIncomeItemDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var supervisor = await _incomeItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .Where(s => s.DzematId == incomeItem.DzematId)
                .FirstOrDefaultAsync();

            if (supervisor == null)
            {
                throw new UnauthorizedAccessException("Not authorized.");
            }

            var spreadsheet = await _incomeItemContext.Spreadsheets.Where(sp => sp.Id == incomeItem.SpreadsheetId).SingleOrDefaultAsync();

            var incomeItemToAdd = _mapper.Map<IncomeItem>(incomeItem);

            spreadsheet.IncomeItems.Add(incomeItemToAdd);
            await _incomeItemContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetIncomeItemDto>(incomeItemToAdd);
            response.Message = "Stavka priliva novca je uspješno kreirana";

            return response;
        }

        public async Task<ServiceResponse<GetIncomeDto>> DeleteIncome(string token, DeleteIncomeDto income)
        {
            var response = new ServiceResponse<GetIncomeDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _incomeItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .Where(s => s.DzematId == income.DzematId)
                .FirstOrDefaultAsync();

            if (supervisor == null)
            {
                throw new UnauthorizedAccessException("Not authorized.");
            }

            var incomeItem = await _incomeItemContext.IncomeItems.Where(e => e.Id == income.IncomeItemId).Include(p => p.Spreadsheet).FirstOrDefaultAsync();

            if (income is null)
            {
                throw new BadRequestException("Priliv novca je nepostojeći");
            }

            var incomeToDelete = await _incomeItemContext.Incomes.Where(i => i.Id == income.Id).FirstOrDefaultAsync();

            if (incomeToDelete is null)
            {
                throw new BadRequestException("Priliv novca je nepostojeći");
            }

            incomeItem.Income.Remove(incomeToDelete);

            await _incomeItemContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetIncomeDto>(incomeToDelete);
            response.Message = "Priliv novca uspješno uklonjen iz baze";

            return response;
        }

        public async Task<ServiceResponse<GetIncomeItemDto>> DeleteIncomeItem(string token, DeleteIncomeItemDto incomeItemToDelete)
        {
            var response = new ServiceResponse<GetIncomeItemDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _incomeItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .Where(s => s.DzematId == incomeItemToDelete.DzematId)
                .FirstOrDefaultAsync();

            if (supervisor == null)
            {
                throw new UnauthorizedAccessException("Not authorized.");
            }

            var incomeItem = await _incomeItemContext.IncomeItems.Where(e => e.Id == incomeItemToDelete.Id).FirstOrDefaultAsync();

            if (incomeItem is null)
            {
                throw new BadRequestException("Stavka je nepostojeća");
            }

            _incomeItemContext.IncomeItems.Remove(incomeItem);

            await _incomeItemContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetIncomeItemDto>(incomeItem);
            response.Message = "Stavka priliva novca je uspješno uklonjena";

            return response;
        }

        public async Task<ServiceResponse<GetActiveIncomeItems>> GetActiveIncomeItems(string token)
        {
            var response = new ServiceResponse<GetActiveIncomeItems>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _incomeItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .FirstOrDefaultAsync();

            if (supervisor is null)
            {
                throw new BadRequestException("Supervisor not found");
            }

            var incomeItems = await _incomeItemContext.Spreadsheets
                .Where(p => p.DzematId == supervisor.DzematId)
                .Where(p => p.Archived == false)
                .Include(s => s.IncomeItems)
                .ThenInclude(e => e.Income)
                .Select(p => new GetActiveIncomeItems
                {
                    IncomeItems = p.IncomeItems.Select(i => _mapper.Map<GetIncomeItemDto>(i)).ToList(),
                    SpreadsheetYear = p.Year,
                    SpreadsheetId = p.Id,
                    TotalIncomeAmountListed = p.IncomeItems.Select(e => e.Income.GroupBy(e => e.IncomeItemId).Select(e => e.Sum(inc => inc.Amount)).ToList()).ToList()
                }).FirstOrDefaultAsync();

            response.Data = incomeItems;
            response.Message = "Aktivni prilivi novca uspješno učitani";

            return response;
        }

        public async Task<ServiceResponse<List<GetIncomeItemDto>>> GetArchivedIncomeItems(string token)
        {
            var response = new ServiceResponse<List<GetIncomeItemDto>>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _incomeItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .FirstOrDefaultAsync();

            if (supervisor is null)
            {
                throw new BadRequestException("Supervisor not found");
            }

            var incomeItems = await _incomeItemContext.IncomeItems
                .Where(p => p.Spreadsheet.DzematId == supervisor.DzematId)
                .Where(p => p.Spreadsheet.Archived == true)
                .Include(p => p.Income)
                .ToListAsync();

            response.Data = incomeItems.Select(item => _mapper.Map<GetIncomeItemDto>(item)).ToList();
            response.Message = "Arhivirani prilivi novca uspješno učitani";

            return response;
        }

        public async Task<ServiceResponse<List<GetDonatorDto>>> GetDonators(string token)
        {
            var response = new ServiceResponse<List<GetDonatorDto>>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _incomeItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .FirstOrDefaultAsync();

            if (supervisor is null)
            {
                throw new BadRequestException("Supervisor not found");
            }

            var donators = await _incomeItemContext.Donators.Where(d => d.DzematId == supervisor.DzematId).ToListAsync();
            
            response.Data = donators.Select(d => _mapper.Map<GetDonatorDto>(d)).ToList();
            response.Message = "Uspješno učitani svi donatori ovog džemata";

            return response;
        }

        public async Task<ServiceResponse<GetDonatorDto>> AddDonator(string token, AddDonatorDto donator)
        {
            var response = new ServiceResponse<GetDonatorDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _incomeItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .Where(s => s.DzematId == donator.DzematId)
                .FirstOrDefaultAsync();

            if (supervisor == null)
            {
                throw new UnauthorizedAccessException("Not authorized.");
            }

            var donatorToAdd = _mapper.Map<Donator>(donator);

            _incomeItemContext.Donators.Add(donatorToAdd);
            await _incomeItemContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetDonatorDto>(donatorToAdd);
            response.Message =  "Uspješno ste dodali donatora";

            return response;
        }
    }
}
