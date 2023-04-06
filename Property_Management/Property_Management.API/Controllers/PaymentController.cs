using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;

namespace Property_Management.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;
        public PaymentController(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }

        [HttpPost("make-payment")]
        public async Task<IActionResult> MakePayment(PaymentRequest request, string paymentFor)
        {
            PaymentResponse response = await _paymentServices.MakePayment(request, paymentFor);
            return Ok(response);
        }

        [HttpGet("get-all-payments")]
        public async Task<IActionResult> GetAllPayment()
        {
            IEnumerable<TransactionResponse> response = await _paymentServices.GetAllPayment();
            return Ok(response);
        }

        [HttpGet("get-payment")]
        public async Task<IActionResult> GetPayment(string Id)
        {
            TransactionResponse response = await _paymentServices.GetPayment(Id);
            return Ok(response);
        }

        [HttpGet("verify-payment")]
        public async Task<IActionResult> VerifyPayment(string reference)
        {
            var response = await _paymentServices.VerifyPayment(reference);
            return Ok(response);
        }

    }
}
