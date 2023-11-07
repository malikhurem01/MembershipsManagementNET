using MembershipsManagement.DTOs.PaymentDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Services.FinanceServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembershipsManagement.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<GetPaymentDto>>> AddPayment(AddPaymentDto payment)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);

            var response = await _paymentService.AddPayment(tokenValue, payment);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<GetPaymentDto>>> DeletePayment(DeletePaymentDto payment)
        {
            var response = await _paymentService.DeletePayment(payment);
            return Ok(response);
        }
    }
}
