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

            var memberInfo = await _context.MembersInfo
                .Where(mi => mi.MemberId == payment.MemberId)
                .Where(mi => mi.SpreadsheetId == payment.SpreadsheetId)
                .FirstOrDefaultAsync();

            memberInfo.TotalAmountPayed += payment.Amount;

            if(memberInfo is null)
            {
                throw new EntityNotFoundException("No join info for member ID: " 
                    + payment.MemberId 
                    + " and spreadsheet ID: " 
                    + payment.SpreadsheetId 
                    + " was found.");
            }

            memberInfo.Payments.Add(paymentToAdd);

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

            if (paymentToDelete is null)
            {
                throw new EntityNotFoundException("Payment with ID: "
                    + payment.Id + " for member ID: "
                    + payment.MemberId + " for spreadsheet ID: "
                    + payment.SpreadsheetId + " was not found.");
            }

            var memberInfo = await _context.MembersInfo
                .Where(mi => mi.MemberId == payment.MemberId)
                .Where(mi => mi.SpreadsheetId == payment.SpreadsheetId)
                .FirstOrDefaultAsync();

            if (memberInfo is null)
            {
                throw new EntityNotFoundException("Payment with ID: "
                    + payment.Id + " for member ID: "
                    + payment.MemberId + " for spreadsheet ID: "
                    + payment.SpreadsheetId + " was not found.");
            }

            memberInfo.TotalAmountPayed -= paymentToDelete.Amount;

            _context.Payments.Remove(paymentToDelete);

            await _context.SaveChangesAsync();

            response.Data = "";
            response.Message = "Payment with ID: "
                    + payment.Id + " for member ID: "
                    + payment.MemberId + " for spreadsheet ID: "
                    + payment.SpreadsheetId + " was successfully deleted.";

            return response;
        }

        public async Task<ServiceResponse<List<GetPaymentDto>>> GetMemberPaymentList(string token, int memberId)
        {
            var response = new ServiceResponse<List<GetPaymentDto>>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var supervisorId = decodedToken.Claims
                            .Where(claim => claim.Type
                            .Equals("nameid"))
                            .Select(claim => claim.Value)
                            .SingleOrDefault();

            var supervisor = await _context.Supervisors.Where(s => s.Id.Equals(supervisorId)).FirstOrDefaultAsync();
            if(supervisor is null)
            {
                throw new EntityNotFoundException("FATAL ERROR: Supervisor with ID: " + supervisorId + " was not found in the database.");
            }

            var member = await _context.Members.Where(m => m.Id == memberId).Where(m => m.DzematId.Equals(supervisor.DzematId)).Include(m => m.Payments).FirstOrDefaultAsync();

            if(member is null)
            {
                throw new EntityNotFoundException("FATAL ERROR: Member with ID: " + member + " in supervisor's (ID: " + supervisorId + ") Dzemat was not found.");
            }

            response.Data = member.Payments.Select(p => _mapper.Map<GetPaymentDto>(p)).ToList();
            response.Message = "Payments for member with ID: " + memberId + " were successfully fetched.";

            return response;
        }

        public async Task<ServiceResponse<List<GetPaymentDto>>> GetMemberSpreadsheetPaymentList(string token, int memberId, int spreadsheetId)
        {
            var response = new ServiceResponse<List<GetPaymentDto>>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var supervisorId = decodedToken.Claims
                            .Where(claim => claim.Type
                            .Equals("nameid"))
                            .Select(claim => claim.Value)
                            .SingleOrDefault();

            var supervisor = await _context.Supervisors.Where(s => s.Id.Equals(supervisorId)).FirstOrDefaultAsync();
            if (supervisor is null)
            {
                throw new EntityNotFoundException("FATAL ERROR: Supervisor with ID: " + supervisorId + " was not found in the database.");
            }

            var member = await _context.Members
                .Where(m => m.Id == memberId)
                .Where(m => m.DzematId
                .Equals(supervisor.DzematId))
                .Include(m => m.Payments
                .Where(p => p.SpreadsheetId == spreadsheetId))
                .FirstOrDefaultAsync();

            if (member is null)
            {
                throw new EntityNotFoundException("FATAL ERROR: Member with ID: " + member + " in supervisor's (ID: " + supervisorId + ") Dzemat was not found.");
            }

            response.Data = member.Payments.Select(p => _mapper.Map<GetPaymentDto>(p)).ToList();
            response.Message = "Payments for member with ID: " + memberId + " for spreadsheet with ID: " + spreadsheetId + " were successfully fetched.";

            return response;
        }

        public Task<ServiceResponse<GetPaymentDto>> GetPayment(string token, int memberId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetPaymentDto>>> GetPaymentSpreadsheetList(string token, int spreadsheetId)
        {
            var response = new ServiceResponse<List<GetPaymentDto>>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var supervisorId = decodedToken.Claims
                            .Where(claim => claim.Type
                            .Equals("nameid"))
                            .Select(claim => claim.Value)
                            .SingleOrDefault();

            var supervisor = await _context.Supervisors.Where(s => s.Id.Equals(supervisorId)).FirstOrDefaultAsync();
            if (supervisor is null)
            {
                throw new EntityNotFoundException("FATAL ERROR: Supervisor with ID: " + supervisorId + " was not found in the database.");
            }

            var spreadsheet = await _context.Spreadsheets
                .Where(s => s.DzematId
                .Equals(supervisor.DzematId))
                .Where(s => s.Id == spreadsheetId)
                .Include(s => s.Payments)
                .FirstOrDefaultAsync();

            if (supervisor is null)
            {
                throw new EntityNotFoundException("FATAL ERROR: Spreadsheet with ID: " + spreadsheetId + " was not found in the database.");
            }

            response.Data = spreadsheet.Payments.Select(p => _mapper.Map<GetPaymentDto>(p)).ToList();
            response.Message = "Payments for spreadsheet with ID: " + spreadsheetId + " were successfully fetched.";

            return response;
        }
    }
}
