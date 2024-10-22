using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class ModifyMemberDto
    {
        public int Id { get; set; }
        public int EvNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? FathersName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public Status Status { get; set; }
        public string? Note { get; set; } = string.Empty;
        public int DzematId { get; set; }
        public bool Active { get; set; } 
    }
}
