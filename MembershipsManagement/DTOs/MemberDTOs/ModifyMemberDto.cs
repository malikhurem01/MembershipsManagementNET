using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class ModifyMemberDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? FathersName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public Status? Status { get; set; }
        public int? MembershipFee { get; set; }
        public int? EvNumber { get; set; }
        public string? DzematId { get; set; } = string.Empty;
    }
}
