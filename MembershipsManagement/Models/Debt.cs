using MembershipsManagement.Models;

namespace MembershipsManagement.Entities
{
    public class Debt
    {
        public int Id { get; set; }
        public int DebtFee { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int SpreadsheetId { get; set; }
        public Spreadsheet Spreadsheet { get; set; }
    }
}
