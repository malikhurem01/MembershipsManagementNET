using MembershipsManagement.DTOs.Member;
using MembershipsManagement.Entities;
using MembershipsManagement.Services.Member;
using Microsoft.AspNetCore.Mvc;

namespace MembershipsManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<GetMemberDto>>> AddMember(AddMemberDto member)
        {
            var response = await _memberService.AddMember(member);
            return Ok(response);
        }

        [HttpGet("get/single")]
        public async Task<ActionResult<ServiceResponse<GetMemberDto>>> GetSingleMember(int memberId)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _memberService.GetSingleMember(tokenValue, memberId);
            return Ok(response);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<GetMemberDto>>>> GetMemberList()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _memberService.GetMemberList(tokenValue);
            return Ok(response);
        }

        [HttpPatch("modify")]
        public async Task<ActionResult<ServiceResponse<GetMemberDto>>> ModifyMember(ModifyMemberDto member)
        {
            var response = await _memberService.ModifyMember(member);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteMember(DeleteMemberDto member)
        {
            var response = await _memberService.DeleteMember(member);
            return Ok(response);
        }
    }
}
