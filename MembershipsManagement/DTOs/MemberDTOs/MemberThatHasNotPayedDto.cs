namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class MemberThatHasNotPayedDto
    {
        public string FullName { get; set; } = string.Empty;
        public double TotalAmount { get; set; }
        public double Debt { get; set; }
    }
}
