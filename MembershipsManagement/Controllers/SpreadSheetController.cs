using MembershipsManagement.DTOs.BazaDTOs;
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
        private readonly ISpreadsheetService _bazaService;

        public SpreadsheetController(ISpreadsheetService bazaService)
        {
            _bazaService = bazaService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> CreateBaza(AddSpreadsheetDto baza) {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _bazaService.CreateBaza(tokenValue, baza);
            return Ok(response);
        }
        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<GetSpreadsheetDto>>>> GetBazaList(int dzematId) {
            var response = await _bazaService.GetBazaList(dzematId);
            return Ok(response);
        }
        [HttpGet("get/single")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> GetSingleBaza(int spreadsheetId) {
            var response = await _bazaService.GetSingleBaza(spreadsheetId);
            return Ok(response);
        }
        [HttpGet("get/active")]
        public async Task<ActionResult<ServiceResponse<object>>> GetActiveSpreadsheet()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _bazaService.GetActiveSpreadsheet(tokenValue);
            return Ok(response);
        }

        [HttpGet("get/report")]
        public async Task<ActionResult<ServiceResponse<SpreadsheetReportDto>>> GetSpreadsheetReport(int spreadsheetYear)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _bazaService.GetSpreadsheetReport(tokenValue, spreadsheetYear);
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
            var response = await _bazaService.GetDebtWarningReports(tokenValue);
            return Ok(response);
        }

        [HttpPatch("archive")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> ArchiveBaza(ArchiveSpreadsheetDto spreadsheet) {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _bazaService.ArchiveBaza(tokenValue, spreadsheet);
            return Ok(response);
        }
        [HttpPatch("unarchive")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> UnarchiveBaza(int bazaId) {
            var response = await _bazaService.UnarchiveBaza(bazaId);
            return Ok(response);
        }
        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteBaza(DeleteSpreadsheetDto baza) {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _bazaService.DeleteBaza(tokenValue, baza);
            return Ok(response);
        }
    }
}
