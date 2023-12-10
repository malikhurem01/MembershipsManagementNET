using MembershipsManagement.DTOs.AuthDTOs;
using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.DTOs.MedzlisDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Services.DzematServiceImpl;
using Microsoft.AspNetCore.Mvc;

namespace MembershipsManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DzematController : ControllerBase
    {
        private readonly IDzematService _dzematService;

        public DzematController(IDzematService dzematService)
        {
            _dzematService = dzematService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<GetDzematDto>>> RegisterDzemat(RegisterDzematDto dzemat)
        {
            var response = await _dzematService.RegisterDzemat(dzemat);
            return Ok(response);
        }
    }
}
