using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.MemberInfoDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.MemberServiceImpl
{
    public interface IMemberService
    {
        Task<ServiceResponse<GetMemberDto>> AddMember(string token, AddMemberDto member);
        Task<ServiceResponse<GetMemberDto>> AddMemberFamily(string token, AddMemberFamilyDto familyMember);
        Task<ServiceResponse<List<GetSpreadsheetMemberInfo>>> GetFilteredMembersForActiveSpredsheet(string token, int spreadsheetId, string? firstName, string? lastName, string? fathersName, int pageNumber);
        Task<ServiceResponse<GetMemberDto>> ModifyMember(ModifyMemberDto memberModified);
        Task<ServiceResponse<List<GetFamilyMemberDto>>> GetFamily(string token, int memberId);
        Task<ServiceResponse<List<GetMemberDto>>> GetMemberList(string token);
        Task<ServiceResponse<GetMemberDto>> GetSingleMember(string token, int memberId);
        Task<ServiceResponse<string>> DeleteMember(DeleteMemberDto member);
        Task<ServiceResponse<GetMemberDto>> DeleteMemberFamily(string token, DeleteFamilyMemberDto familyMember);
    }
}
