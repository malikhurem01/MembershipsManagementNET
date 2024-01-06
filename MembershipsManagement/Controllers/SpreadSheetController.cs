using MembershipsManagement.DTOs.SpreadsheetDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Services.SpreadSheets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembershipsManagement.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SpreadsheetController : Controller
    {
        private readonly ISpreadsheetService _spreadsheetService;

        public SpreadsheetController(ISpreadsheetService spreadsheetService)
        {
            _spreadsheetService = spreadsheetService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> CreateSpreadsheet(AddSpreadsheetDto spreadsheet) {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _spreadsheetService.CreateSpreadsheet(tokenValue, spreadsheet);
            return Ok(response);
        }
        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<GetSpreadsheetDto>>>> GetSpreadsheetList(int dzematId) {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _spreadsheetService.GetSpreadsheetList(tokenValue, dzematId);
            return Ok(response);
        }
        [HttpGet("get/single")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> GetSingleSpreadsheet(int spreadsheetId) {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _spreadsheetService.GetSingleSpreadsheet(tokenValue, spreadsheetId);
            return Ok(response);
        }
        [HttpGet("get/active")]
        public async Task<ActionResult<ServiceResponse<object>>> GetActiveSpreadsheet(int? dzematId)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _spreadsheetService.GetActiveSpreadsheet(tokenValue, dzematId);
            return Ok(response);
        }

        [HttpGet("get/report")]
        public async Task<ActionResult<ServiceResponse<SpreadsheetReportDto>>> GetSpreadsheetReport(int spreadsheetYear, int? dzematId)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _spreadsheetService.GetSpreadsheetReport(tokenValue, spreadsheetYear, dzematId);
            return Ok(response);
        }

        [HttpGet("get/debt-warning")]
        public async Task<ActionResult<ServiceResponse<SpreadsheetReportDto>>> GetDebtWarningReports()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _spreadsheetService.GetDebtWarningReports(tokenValue);
            return Ok(response);
        }

        [HttpPatch("archive")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> ArchiveSpreadsheet(ArchiveSpreadsheetDto spreadsheet) {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _spreadsheetService.ArchiveSpreadsheet(tokenValue, spreadsheet);
            return Ok(response);
        }
        [HttpPatch("unarchive")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> UnarchiveSpreadsheet(int spreadsheetId) {
            var response = await _spreadsheetService.UnarchiveSpreadsheet(spreadsheetId);
            return Ok(response);
        }
        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteSpreadsheet(DeleteSpreadsheetDto spreadsheet) {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _spreadsheetService.DeleteSpreadsheet(tokenValue, spreadsheet);
            return Ok(response);
        }
    }
}
