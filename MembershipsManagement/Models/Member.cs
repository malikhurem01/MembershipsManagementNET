namespace MembershipsManagement.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public int EvNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Active { get; set; }
        public double Debt { get; set; }
        public Status Status { get; set; }
        public List<Payment>? Payments { get; set; } = new List<Payment>();
        public List<FamilyMember>? FamilyMembers { get; set; } = new List<FamilyMember>();
        public List<Spreadsheet>? Spreadsheets { get; set; } = new List<Spreadsheet>();
        public List<MemberInfo>? MemberInfo { get; set; } = new List<MemberInfo>();
        public string DzematId { get; set; }
        public Dzemat Dzemat { get; set; }
    }
}
