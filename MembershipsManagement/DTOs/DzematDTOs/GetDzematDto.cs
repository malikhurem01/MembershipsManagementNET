using MembershipsManagement.DTOs.SpreadsheetDTOs;
using MembershipsManagement.DTOs.MedzlisDTOs;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.User;

namespace MembershipsManagement.DTOs.DzematDTOs
{
    public class GetDzematDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int MedzlisId { get; set; }
        public GetMedzlisDto Medzlis { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public int? ZipCode { get; set; }
        public List<GetMemberDto>? Members { get; set; } = new List<GetMemberDto>();
        public List<GetUserDto>? Supervisors { get; set; } = new List<GetUserDto>();
        public List<GetSpreadsheetDto>? Spreadsheets { get; set; } = new List<GetSpreadsheetDto>();
        public double? Budget { get; set; }
    }
}
