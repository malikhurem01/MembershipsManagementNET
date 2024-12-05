using MembershipsManagement.Entities;
using MembershipsManagement.Models;

namespace MembershipsManagement.DTOs.PaymentDTOs
{
    public class GetPaymentDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime DateOfPayment { get; set; }
        public string MemberId { get; set; }
        public Member Member { get; set; }
        public string SupervisorId { get; set; } = string.Empty;
        public Supervisor? Supervisor { get; set; }
        public int SpreadsheetId { get; set; }
        public Spreadsheet Spreadsheet { get; set; }
    }
}
