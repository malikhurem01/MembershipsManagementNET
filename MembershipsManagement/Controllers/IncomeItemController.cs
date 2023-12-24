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
    public class IncomeItemController : ControllerBase
    {
        private readonly IIncomeItemService _incomeItemService;

        public IncomeItemController(IIncomeItemService incomeItemService)
        {
            this._incomeItemService = incomeItemService;
        }

        [HttpGet("active")]
        public async Task<ActionResult<ServiceResponse<GetActiveIncomeItems>>> GetActiveIncomeItems()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _incomeItemService.GetActiveIncomeItems(tokenValue);
            return response;
        }

        [HttpGet("archived")]
        public async Task<ActionResult<ServiceResponse<List<GetIncomeItemDto>>>> GetArchivedIncomeItems()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _incomeItemService.GetArchivedIncomeItems(tokenValue);
            return response;
        }

        [HttpGet("donators")]
        public async Task<ActionResult<ServiceResponse<List<GetDonatorDto>>>> GetDonators()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _incomeItemService.GetDonators(tokenValue);
            return response;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<GetIncomeItemDto>>> CreateIncomeItem(CreateIncomeItem item)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _incomeItemService.CreateIncomeItem(tokenValue, item);
            return response;
        }

        [HttpPost("donator")]
        public async Task<ActionResult<ServiceResponse<GetDonatorDto>>> AddDonator(AddDonatorDto donator)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _incomeItemService.AddDonator(tokenValue, donator);
            return response;
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ServiceResponse<GetIncomeItemDto>>> DeleteIncomeItem(DeleteIncomeItemDto item)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _incomeItemService.DeleteIncomeItem(tokenValue, item);
            return response;
        }

        [HttpPost("add/income")]
        public async Task<ActionResult<ServiceResponse<GetIncomeDto>>> AddIncome(AddIncomeDto income)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _incomeItemService.AddIncome(tokenValue, income);
            return response;
        }

        [HttpPost("delete/income")]
        public async Task<ActionResult<ServiceResponse<GetIncomeDto>>> DeleteIncome(DeleteIncomeDto income)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _incomeItemService.DeleteIncome(tokenValue, income);
            return response;
        }
    }
}
