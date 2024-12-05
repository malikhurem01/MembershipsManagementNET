using MembershipsManagement.Models;

namespace MembershipsManagement.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime DateOfPayment { get; set; }
        public string MemberId { get; set; } = string.Empty;
        public Member Member { get; set; }
        public int SpreadsheetId { get; set; }
        public Spreadsheet Spreadsheet { get; set; }
        public string SupervisorId { get; set; } = string.Empty;
        public Supervisor Supervisor { get; set; }
        public int MemberInfoId { get; set; }
        public MemberInfo MemberInfo { get; set; }
    }
}
