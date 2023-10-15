using MembershipsManagement.DTOs.MedzlisDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Services.MedzlisServiceImpl;
using Microsoft.AspNetCore.Mvc;

namespace MembershipsManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedzlisController : ControllerBase
    {
        private readonly IMedzlisService _medzlisService;

        public MedzlisController(IMedzlisService medzlisService)
        {
            _medzlisService = medzlisService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<GetMedzlisDto>>> CreateMedzlis(AddMedzlisDto medzlis)
        {
            var response = await _medzlisService.CreateMedzlis(medzlis);
            return Ok(response);
        }

        [HttpGet("get/single")]
        public async Task<ActionResult<ServiceResponse<GetMedzlisDto>>> GetMedzlis(int medzlisId)
        {
            var response = await _medzlisService.GetMedzlis(medzlisId);
            return Ok(response);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<GetMedzlisDto>>> GetMedzlisList()
        {
            var response = await _medzlisService.GetMedzlisList();
            return Ok(response);
        }

        [HttpPatch("modify")]
        public async Task<ActionResult<ServiceResponse<GetMedzlisDto>>> ModifyMedzlis(ModifyMedzlisDto medzlis)
        {
            var response = await _medzlisService.ModifyMedzlis(medzlis);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<GetMedzlisDto>>> DeleteMedzlis(int medzlisId)
        {
            var response = await _medzlisService.DeleteMedzlis(medzlisId);
            return Ok(response);
        }
    }
}
