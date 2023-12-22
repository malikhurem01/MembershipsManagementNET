using AutoMapper;
using AutoMapper.Execution;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.ProjectDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;

namespace MembershipsManagement.Services.ProjectServiceImpl
{
    public class ExpenseItemService : IExpenseItemService
    {
        private readonly DataContext _expenseItemContext;
        private readonly IMapper _mapper;

        public ExpenseItemService(DataContext expenseItemContext, IMapper mapper)
        {
            _expenseItemContext = expenseItemContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetExpenseItemDto>> CreateExpenseItem(string token, CreateExpenseItem project)
        {
            var response = new ServiceResponse<GetExpenseItemDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var supervisor = await _expenseItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .Where(s => s.DzematId == project.DzematId)
                .FirstOrDefaultAsync();

            if (supervisor == null)
            {
                throw new UnauthorizedAccessException("Not authorized.");
            }

            var spreadsheet = await _expenseItemContext.Spreadsheets.Where(sp => sp.Id == project.SpreadsheetId).SingleOrDefaultAsync();

            var projectToAdd = _mapper.Map<ExpenseItem>(project);

            spreadsheet.ExpenseItems.Add(projectToAdd);
            await _expenseItemContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetExpenseItemDto>(projectToAdd);
            response.Message = "Projekat je uspješno kreiran";

            return response;
        }

        /*public async Task<ServiceResponse<GetIncomeDto>> AddIncome(string token, AddIncomeDto income)
        {
            var response = new ServiceResponse<GetIncomeDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _expenseItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .Where(s => s.DzematId == income.DzematId)
                .FirstOrDefaultAsync();

            if (supervisor == null)
            {
                throw new UnauthorizedAccessException("Not authorized.");
            }

            var project = await _expenseItemContext.Projects.Where(p => p.Id == income.ProjectId).Include(p => p.Spreadsheet).FirstOrDefaultAsync();
            
            var incomeToAdd = _mapper.Map<Income>(income);
            incomeToAdd.Supervisor = supervisor;
            project.Incomes.Add(incomeToAdd);

            await _expenseItemContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetIncomeDto>(incomeToAdd);
            response.Message = "Priliv novca je upsješno zaveden";

            return response;
        } */

        public async Task<ServiceResponse<GetExpenseDto>> AddExpense(string token, AddExpenseDto expense)
        {
            var response = new ServiceResponse<GetExpenseDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _expenseItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .Where(s => s.DzematId == expense.DzematId)
                .FirstOrDefaultAsync();

            if (supervisor == null)
            {
                throw new UnauthorizedAccessException("Not authorized.");
            }

            var expenseItem = await _expenseItemContext.ExpenseItems.Where(p => p.Id == expense.ExpenseItemId).Include(p => p.Spreadsheet).FirstOrDefaultAsync();

            var expenseToAdd = _mapper.Map<Expense>(expense);
            expenseToAdd.Supervisor = supervisor;
            expenseItem.Expenses.Add(expenseToAdd);

            await _expenseItemContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetExpenseDto>(expenseToAdd);
            response.Message = "Odliv novca je upsješno zaveden";

            return response;
        }

        /*public async Task<ServiceResponse<GetIncomeDto>> DeleteIncome(string token, DeleteIncomeDto income)
        {
            var response = new ServiceResponse<GetIncomeDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _expenseItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .Where(s => s.DzematId == income.DzematId)
                .FirstOrDefaultAsync();

            if (supervisor == null)
            {
                throw new UnauthorizedAccessException("Not authorized.");
            }

            var project = await _expenseItemContext.Projects.Include(p => p.Spreadsheet).FirstOrDefaultAsync();

            if(project is null)
            {
                throw new BadRequestException("Projekat je nepostojeći");
            }

            var incomeToDelete = await _expenseItemContext.Incomes.Where(i => i.Id == income.Id).FirstOrDefaultAsync();

            if(incomeToDelete is null)
            {
                throw new BadRequestException("Priliv je nepostojeći");
            }

            project.Incomes.Remove(incomeToDelete);

            await _expenseItemContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetIncomeDto>(incomeToDelete);
            response.Message = "Priliv novca uspješno uklonjen iz baze";

            return response;

        } */

        public async Task<ServiceResponse<GetExpenseDto>> DeleteExpense(string token, DeleteExpenseDto expense)
        {
            var response = new ServiceResponse<GetExpenseDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _expenseItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .Where(s => s.DzematId == expense.DzematId)
                .FirstOrDefaultAsync();

            if (supervisor == null)
            {
                throw new UnauthorizedAccessException("Not authorized.");
            }

            var expenseItem = await _expenseItemContext.ExpenseItems.Include(p => p.Spreadsheet).FirstOrDefaultAsync();

            if (expense is null)
            {
                throw new BadRequestException("Projekat je nepostojeći");
            }

            var expenseToDelete = await _expenseItemContext.Expenses.Where(i => i.Id == expense.Id).FirstOrDefaultAsync();

            if (expenseToDelete is null)
            {
                throw new BadRequestException("Odliv je nepostojeći");
            }

            expenseItem.Expenses.Remove(expenseToDelete);

            await _expenseItemContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetExpenseDto>(expenseToDelete);
            response.Message = "Odliv novca uspješno uklonjen iz baze";

            return response;
        }

        public async Task<ServiceResponse<GetActiveExpenseItems>> GetActiveExpenseItems(string token)
        {
            var response = new ServiceResponse<GetActiveExpenseItems>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _expenseItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .FirstOrDefaultAsync();

            if(supervisor is null)
            {
                throw new BadRequestException("Supervisor not found");
            }

            var expenseItems = await _expenseItemContext.Spreadsheets
                .Where(p => p.DzematId == supervisor.DzematId)
                .Where(p => p.Archived == false)
                .Include(s => s.ExpenseItems)
                .ThenInclude(e => e.Expenses)
                .Select(p => new GetActiveExpenseItems
                {
                    ExpenseItems = p.ExpenseItems.Select(e => _mapper.Map<GetExpenseItemDto>(e)).ToList(),
                    SpreadsheetYear = p.Year,
                    SpreadsheetId = p.Id,
                    TotalExpensesAmountListed = p.ExpenseItems.Select(e => e.Expenses.GroupBy(e => e.ExpenseItemId).Select(e => e.Sum(exp => exp.Amount)).ToList()).ToList()
                }).FirstOrDefaultAsync();
                
            response.Data = expenseItems;
            response.Message = "Aktivni troškovi uspješno učitani";

            return response;
            
        }
        public async Task<ServiceResponse<List<GetExpenseItemDto>>> GetArchivedExpenseItems(string token)
        {
            var response = new ServiceResponse<List<GetExpenseItemDto>>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();


            var supervisor = await _expenseItemContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .FirstOrDefaultAsync();

            if (supervisor is null)
            {
                throw new BadRequestException("Supervisor not found");
            }

            var expenseItems = await _expenseItemContext.ExpenseItems
                .Where(p => p.Spreadsheet.DzematId == supervisor.DzematId)
                .Where(p => p.Spreadsheet.Archived == true)
                .Include(p => p.Expenses)
                .ToListAsync();

            response.Data = expenseItems.Select(project => _mapper.Map<GetExpenseItemDto>(project)).ToList();
            response.Message = "Arhivirani projekti uspješno učitani";

            return response;
        }
    }
}
