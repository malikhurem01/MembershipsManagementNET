namespace MembershipsManagement.DTOs.PaymentDTOs
{
    public class DeletePaymentDto
    {
        public int Id { get; set; }
        public string MemberId { get; set; }
        public int SpreadsheetId { get; set; }
    }
}
