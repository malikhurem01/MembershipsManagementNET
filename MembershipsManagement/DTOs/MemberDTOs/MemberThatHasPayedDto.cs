namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class MemberThatHasPayedDto
    {
        public string FullName { get; set; } = string.Empty;
        public IEnumerable<string> Supervisors { get; set; } = new List<string>();
        public double? TotalAmount { get; set; }
    }
}
