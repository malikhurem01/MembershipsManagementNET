using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class AddMemberDto
    {
        public int EvNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int MembershipFee { get; set; }
        public double Debt { get; set; }
        public bool PaymentMade { get; set; }
        public Status Status { get; set; }        
        public List<AddMemberFamilyListDto>? FamilyMembers { get; set; } = new List<AddMemberFamilyListDto>();
        public string DzematId { get; set; }
        public bool AddToSpreadsheet { get; set; }
        public bool Active { get; set; }
    }
}
