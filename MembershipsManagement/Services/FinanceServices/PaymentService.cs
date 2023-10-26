using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.PaymentDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace MembershipsManagement.Services.FinanceServices
{
    public class PaymentService : IPaymentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PaymentService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetPaymentDto>> AddPayment(string token, AddPaymentDto payment)
        {
            var response = new ServiceResponse<GetPaymentDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var supervisorId = decodedToken.Claims
                            .Where(claim => claim.Type
                            .Equals("nameid"))
                            .Select(claim => claim.Value)
                            .SingleOrDefault();

            var paymentToAdd = _mapper.Map<Payment>(payment);
            paymentToAdd.SupervisorId = supervisorId;

            _context.Payments.Add(paymentToAdd);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetPaymentDto>(paymentToAdd);
            response.Message = "Payment has been successfully added.";

            return response;
        }

        public async Task<ServiceResponse<string>> DeletePayment(DeletePaymentDto payment)
        {
            var response = new ServiceResponse<string>();
            var paymentToDelete = await _context.Payments
                .Where(p => p.Id == payment.Id)
                .Where(p => p.MemberId == payment.MemberId)
                .Where(p => p.SpreadsheetId == payment.SpreadsheetId)
                .FirstOrDefaultAsync();

            if(paymentToDelete is null)
            {
                throw new EntityNotFoundException("Payment with ID: " 
                    + payment.Id + " for member ID: " 
                    + payment.MemberId + " for spreadsheet ID: " 
                    + payment.SpreadsheetId + " was not found.");
            }

            _context.Payments.Remove(paymentToDelete);
            await _context.SaveChangesAsync();

            response.Data = "";
            response.Message = "Payment with ID: "
                    + payment.Id + " for member ID: "
                    + payment.MemberId + " for spreadsheet ID: "
                    + payment.SpreadsheetId + " was successfully deleted.";

            return response;
        }

        public Task<ServiceResponse<List<GetPaymentDto>>> GetMemberPaymentList(string token, int memberId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetPaymentDto>>> GetMemberSpreadsheetPaymentList(string token, int memberId, int spreadsheetId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetPaymentDto>> GetPayment(string token, int memberId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetPaymentDto>>> GetPaymentList(string token, int spreadsheetId)
        {
            throw new NotImplementedException();
        }
    }
}
