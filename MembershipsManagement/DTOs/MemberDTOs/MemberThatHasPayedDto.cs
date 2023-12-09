namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class MemberThatHasPayedDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Supervisor { get; set; } = string.Empty;
        public double? TotalAmount { get; set; }
    }
}
