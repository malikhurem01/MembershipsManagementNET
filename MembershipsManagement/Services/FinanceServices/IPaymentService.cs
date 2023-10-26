using MembershipsManagement.DTOs.PaymentDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.FinanceServices
{
    public interface IPaymentService
    {
        Task<ServiceResponse<GetPaymentDto>> AddPayment(string token, AddPaymentDto payment);
        Task<ServiceResponse<List<GetPaymentDto>>> GetPaymentList(string token, int spreadsheetId);
        Task<ServiceResponse<List<GetPaymentDto>>> GetMemberPaymentList(string token, int memberId);
        Task<ServiceResponse<List<GetPaymentDto>>> GetMemberSpreadsheetPaymentList(string token, int memberId,  int spreadsheetId);
        Task<ServiceResponse<GetPaymentDto>> GetPayment(string token, int memberId);
        Task<ServiceResponse<string>> DeletePayment(DeletePaymentDto payment);
    }
}

// ZA URADITI NAPRAVITI GET PAYMENTS BY LIST AND BY ID