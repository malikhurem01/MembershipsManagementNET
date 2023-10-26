using MembershipsManagement.DTOs.MedzlisDTOs;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Models;

namespace MembershipsManagement.DTOs.DzematDTOs
{
    public class GetDzematDto
    {
        public string Id { get; set; } = string.Empty;
        public int MedzlisId { get; set; }
        public GetMedzlisDto Medzlis { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public int? ZipCode { get; set; }
        public List<GetMemberDto>? Members { get; set; } = new List<GetMemberDto>();
        public List<Supervisor>? Supervisors { get; set; } = new List<Supervisor>();
        public List<Spreadsheet>? Spreadsheets { get; set; } = new List<Spreadsheet>();
        public double? Budget { get; set; }
    }
}
