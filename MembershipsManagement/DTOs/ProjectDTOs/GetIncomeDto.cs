using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.User;
using MembershipsManagement.Entities;
using MembershipsManagement.Models;

namespace MembershipsManagement.DTOs.ProjectDTOs
{
    public class GetIncomeDto
    {
        public int Id { get; set; }
        public int IncomeItemId { get; set; }
        public GetIncomeItemDto IncomeItem { get; set; }
        public string SupervisorId { get; set; } = string.Empty;
        public GetUserDto Supervisor { get; set; }
        public int? DonatorId { get; set; }
        public GetDonatorDto? Donator { get; set; }
        public int? MemberId { get; set; }
        public GetMemberDto? Member { get; set; }
        public string? Note { get; set; } = string.Empty;
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public IncomeType IncomeType { get; set; }
    }
}
