namespace MembershipsManagement.Entities
{
    public class FamilyMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public FamilyStatus Status { get; set; }
        public string MemberId { get; set; }
        public Member Member { get; set; }
    }
}
