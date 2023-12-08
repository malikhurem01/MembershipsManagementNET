namespace MembershipsManagement.DTOs.PaymentDTOs
{
    public class GetSpreadsheetPaymentDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime DateOfPayment { get; set; }
        public string SupervisorFirstName { get; set; } = string.Empty;
        public string SupervisorLastName { get; set; } = string.Empty;
    }
}
