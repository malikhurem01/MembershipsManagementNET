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
        private readonly IBazaService _bazaService;

        public SpreadSheetController(IBazaService bazaService)
        {
            _bazaService = bazaService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<GetBazaDto>>> CreateBaza(AddBazaDto baza) {
            var response = await _bazaService.CreateBaza(baza);
            return Ok(response);
        }
        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<GetBazaDto>>>> GetBazaList(int dzematId) {
            var response = await _bazaService.GetBazaList(dzematId);
            return Ok(response);
        }
        [HttpGet("get/single")]
        public async Task<ActionResult<ServiceResponse<GetBazaDto>>> GetSingleBaza(int bazaId) {
            var response = await _bazaService.GetSingleBaza(bazaId);
            return Ok(response);
        }
        [HttpPatch("archive")]
        public async Task<ActionResult<ServiceResponse<GetBazaDto>>> ArchiveBaza(int bazaId) {
            var response = await _bazaService.ArchiveBaza(bazaId);
            return Ok(response);
        }
        [HttpPatch("unarchive")]
        public async Task<ActionResult<ServiceResponse<GetBazaDto>>> UnarchiveBaza(int bazaId) {
            var response = await _bazaService.UnarchiveBaza(bazaId);
            return Ok(response);
        }
        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteBaza(DeleteBazaDto baza) {
            var response = await _bazaService.DeleteBaza(baza);
            return Ok(response);
        }
    }
}
