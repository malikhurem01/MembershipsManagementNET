using MembershipsManagement.DTOs;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.MemberInfoDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.MemberServiceImpl
{
    public interface IMemberService
    {
        Task<object> Populate(int dzematId);
        Task<ServiceResponse<string>> ImportMembersFromCSV(string dzematname);
        Task<ServiceResponse<GetMemberDto>> AddMember(string token, AddMemberDto member);
        Task<ServiceResponse<GetMemberDto>> AddMemberFamily(string token, AddMemberFamilyDto familyMember);
        Task<ServiceResponse<PagedList<GetSpreadsheetMemberInfo>>> GetFilteredMembersForSpredsheet(string token, int spreadsheetId, string? evNumber, string? firstName, string? lastName, string? fathersName, bool? hasPayed, int pageNumber, int pageSize);
        Task<ServiceResponse<GetMemberDto>> ModifyMember(ModifyMemberDto memberModified);
        Task<ServiceResponse<List<GetFamilyMemberDto>>> GetFamily(string token, string memberId);
        Task<ServiceResponse<List<GetMemberDto>>> GetMemberList(string token);
        Task<ServiceResponse<GetMemberDto>> GetSingleMember(string token, string memberId);
        Task<ServiceResponse<string>> DeleteMember(DeleteMemberDto member);
        Task<ServiceResponse<GetMemberDto>> DeleteMemberFamily(string token, DeleteFamilyMemberDto familyMember);
        Task<ServiceResponse<MemberInfoReport>> GetMemberInfoReport(string tokenValue);
    }
}
