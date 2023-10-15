using MembershipsManagement.DTOs.BazaDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Services.SpreadSheets;
using Microsoft.AspNetCore.Mvc;

namespace MembershipsManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpreadSheetController : Controller
    {
        private readonly ISpreadsheetService _bazaService;

        public SpreadSheetController(ISpreadsheetService bazaService)
        {
            _bazaService = bazaService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> CreateBaza(AddSpreadsheetDto baza) {
            var response = await _bazaService.CreateBaza(baza);
            return Ok(response);
        }
        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<GetSpreadsheetDto>>>> GetBazaList(string dzematId) {
            var response = await _bazaService.GetBazaList(dzematId);
            return Ok(response);
        }
        [HttpGet("get/single")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> GetSingleBaza(int bazaId) {
            var response = await _bazaService.GetSingleBaza(bazaId);
            return Ok(response);
        }
        [HttpPatch("archive")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> ArchiveBaza(int bazaId) {
            var response = await _bazaService.ArchiveBaza(bazaId);
            return Ok(response);
        }
        [HttpPatch("unarchive")]
        public async Task<ActionResult<ServiceResponse<GetSpreadsheetDto>>> UnarchiveBaza(int bazaId) {
            var response = await _bazaService.UnarchiveBaza(bazaId);
            return Ok(response);
        }
        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteBaza(DeleteSpreadsheetDto baza) {
            var response = await _bazaService.DeleteBaza(baza);
            return Ok(response);
        }
    }
}
