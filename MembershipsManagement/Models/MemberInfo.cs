namespace MembershipsManagement.Entities
{
    public class MemberInfo
    {
        public int Id { get; set; }
        public int SpreadsheetId { get; set; }
        public Spreadsheet Spreadsheet { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public bool HasDebt { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
