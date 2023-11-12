namespace MembershipsManagement.Entities
{
    public class Spreadsheet
    {
        public int Id { get; set; }
        public bool Archived { get; set; }
        public int Year { get; set; }
        public string DzematId { get; set; }
        public int MarriageFee { get; set; }
        public int WidowerFee { get; set; }
        public int AgeLimitFee { get; set; }
        public Dzemat Dzemat { get; set; }
        public List<Payment>? Payments { get; set; } = new List<Payment>();
        public List<Member>? Members { get; set; } = new List<Member>();
        public List<MemberInfo>? MemberInfo { get; set; } = new List<MemberInfo>();
        public List<Project>? Projects { get; set; } = new List<Project>();
    }
}
