using CsvHelper;
using MembershipsManagement.DTOs;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.MemberInfoDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Services.MemberServiceImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;

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

        [HttpGet("view")]
        public async Task<ActionResult<ServiceResponse<MemberInfoReport>>> GetMemberInfoReport(string memberId)
        {
            var response = await _memberService.GetMemberInfoReport(memberId);
            return Ok(response);
        }

        [HttpPost("csv")]
        public async Task<ActionResult<ServiceResponse<string>>> GetMemebrsFromCSV(string dzematname)
        {
            var response = await _memberService.ImportMembersFromCSV(dzematname);
            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<GetMemberTemporaryCredentials>>> AddMember(AddMemberDto member)
        {
            /*Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()tokenValue
                .Split(" ")
                .ElementAt(1);*/
            var response = await _memberService.AddMember("", member);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("populate")]
        public async Task<ActionResult<object>> Populate(int dzematId)
        {
            var response = await _memberService.Populate(dzematId);
            return response;
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
        [HttpPost("family/add")]
        public async Task<ActionResult<ServiceResponse<GetMemberDto>>> AddMemberFamily(AddMemberFamilyDto familyMember)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _memberService.AddMemberFamily(tokenValue, familyMember);
            return Ok(response);
        }

        [HttpPost("family/delete")]
        public async Task<ActionResult<ServiceResponse<GetMemberDto>>> DeleteMemberFamily(DeleteFamilyMemberDto familyMember)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _memberService.DeleteMemberFamily(tokenValue, familyMember);
            return Ok(response);
        }

        [HttpGet("get/single")]
        public async Task<ActionResult<ServiceResponse<GetMemberDto>>> GetSingleMember(string memberId)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _memberService.GetSingleMember(tokenValue, memberId);
            return Ok(response);
        }

        [HttpGet("get/spreadsheet")]
        public async Task<ActionResult<ServiceResponse<PagedList<GetSpreadsheetMemberInfo>>>> GetFilteredMembersForActiveSpredsheet(int spreadsheetId, string? evNumber, string? firstName, string? lastName, string? fathersName, bool? hasPayed, int pageNumber, int pageSize)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _memberService.GetFilteredMembersForSpredsheet(tokenValue, spreadsheetId, evNumber, firstName, lastName, fathersName, hasPayed, pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("get/family")]
        public async Task<ActionResult<ServiceResponse<List<GetFamilyMemberDto>>>> GetFamily(string memberId)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _memberService.GetFamily(tokenValue, memberId);
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
