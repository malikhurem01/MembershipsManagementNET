using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.MemberServiceImpl
{
    public interface IMemberService
    {
        Task<ServiceResponse<GetMemberDto>> AddMember(string token, AddMemberDto member);
        Task<ServiceResponse<GetMemberDto>> AddMemberFamily(string token, AddMemberFamilyDto familyMember);
        Task<ServiceResponse<GetMemberDto>> DeleteMemberFamily(string token, DeleteFamilyMemberDto familyMember);
        Task<ServiceResponse<object>> GetMemberByNameAndLastName(string token, string name, string lastName, int spreadsheetId);
        Task<ServiceResponse<List<GetFamilyMemberDto>>> GetFamily(string token, int memberId);
        Task<ServiceResponse<List<GetMemberDto>>> GetMemberList(string token);
        Task<ServiceResponse<GetMemberDto>> GetSingleMember(string token, int memberId);
        Task<ServiceResponse<string>> DeleteMember(DeleteMemberDto member);
        Task<ServiceResponse<object>> ModifyMember(ModifyMemberDto memberModified);
        Task<ServiceResponse<object>> GetFilteredMembersForActiveSpredsheet(string token, int spreadsheetId, string? firstName, string? lastName, int pageNumber);
    }
}
