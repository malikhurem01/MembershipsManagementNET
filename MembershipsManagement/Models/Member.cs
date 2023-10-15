namespace MembershipsManagement.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public int EvNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public Status Status { get; set; }
        public int MembershipFee { get; set; }
        public List<Payment>? Payments { get; set; } = new List<Payment>();
        public List<FamilyMember>? FamilyMembers { get; set; } = new List<FamilyMember>();
        public List<Spreadsheet>? Spreadsheets { get; set; } = new List<Spreadsheet>();
        public string? DzematId { get; set; }
        public Dzemat? Dzemat { get; set; }
    }
}
