using MembershipsManagement.DTOs.ProjectDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Services.ProjectServiceImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembershipsManagement.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ExpenseItemController:ControllerBase
    {
        private readonly IExpenseItemService _expenseItemService;

        public ExpenseItemController(IExpenseItemService expenseItemService)
        {
            this._expenseItemService = expenseItemService;
        }

        [HttpGet("active")]
        public async Task<ActionResult<ServiceResponse<GetActiveExpenseItems>>> GetActivedExpenseItems()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _expenseItemService.GetActiveExpenseItems(tokenValue);
            return response;
        }

        [HttpGet("archived")]
        public async Task<ActionResult<ServiceResponse<List<GetExpenseItemDto>>>> GetArchivedExpenseItems()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _expenseItemService.GetArchivedExpenseItems(tokenValue);
            return response;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<GetExpenseItemDto>>> CreateExpenseItem(CreateExpenseItem project)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _expenseItemService.CreateExpenseItem(tokenValue, project);
            return response;
        }

        /* [HttpPost("add/income")]
        public async Task<ActionResult<ServiceResponse<GetIncomeDto>>> AddIncome(AddIncomeDto income)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _expenseItemService.AddIncome(tokenValue, income);
            return response;
        }*/

        [HttpPost("add/expense")]
        public async Task<ActionResult<ServiceResponse<GetExpenseDto>>> AddExpense(AddExpenseDto expense)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _expenseItemService.AddExpense(tokenValue, expense);
            return response;
        }

        /*[HttpPost("delete/income")]
        public async Task<ActionResult<ServiceResponse<GetIncomeDto>>> DeleteIncome (DeleteIncomeDto income)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _expenseItemService.DeleteIncome(tokenValue, income);
            return response;
        }*/

        [HttpPost("delete/expense")]
        public async Task<ActionResult<ServiceResponse<GetExpenseDto>>> DeleteExpense(DeleteExpenseDto expense)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _expenseItemService.DeleteExpense(tokenValue, expense);
            return response;
        }
    }
}
