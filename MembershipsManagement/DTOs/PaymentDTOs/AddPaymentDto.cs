using MembershipsManagement.Entities;
using MembershipsManagement.Models;

namespace MembershipsManagement.DTOs.PaymentDTOs
{
    public class AddPaymentDto
    {
        public double Amount { get; set; }
        public DateTime DateOfPayment { get; set; }
        public string MemberId { get; set; }
        public int SpreadsheetId { get; set; }
    }
}
