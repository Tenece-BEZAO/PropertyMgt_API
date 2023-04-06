using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpPost("make-payment", Name = "make payment")]
        [SwaggerOperation(Summary = "make payment")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "{request.Name} your Payment link has been generated: {response.Data.AuthorizationUrl} use this link to complete your payment.", Type = typeof(PaymentResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Sorry! error occured while processing your request.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> MakePayment(PaymentRequest request, string paymentFor)
        {
            PaymentResponse response = await _paymentServices.MakePayment(request, paymentFor);
            return Ok(response);
        }

        [HttpGet("get-all-payments", Name = "fetch all payment")]
        [SwaggerOperation(Summary = "Get all payment in the database.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "", Type = typeof(IEnumerable<TransactionResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Empty transaction list.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllPayment()
        {
            IEnumerable<TransactionResponse> response = await _paymentServices.GetAllPayment();
            return Ok(response);
        }

        [HttpGet("get-payment", Name = "fetch payment by payment id.")]
        [SwaggerOperation(Summary = "Get all payment in the database.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Product fetched", Type = typeof(TransactionResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Empty transaction list. this transaction id does not exist.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetPayment(string Id)
        {
            TransactionResponse response = await _paymentServices.GetPayment(Id);
            return Ok(response);
        }

        [HttpGet("verify-payment", Name = "Verify payment")]
        [SwaggerOperation(Summary = "Verify payment by reference key. Make payment controller generates a reference key ones a user makes payment.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Payment verification succesful.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Sorry! verification failed. Error occured while trying to verify the transaction. It seems your payment was not successful.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> VerifyPayment(string reference)
        {
            Response response = await _paymentServices.VerifyPayment(reference);
            return Ok(response);
        }

    }
}
