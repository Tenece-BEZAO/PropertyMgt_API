using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Property_Management.API.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailServices _emailServices;
        public EmailController(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }

        [HttpPost("send-email")]
        [SwaggerOperation(Summary = "Send email", Description = "Sends email to user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Sent = true", Type = typeof(EmailResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> SendMail([FromBody] EmailRequests emailRequest)
        {
            EmailResponse response = await _emailServices.SendMailAsync(emailRequest);
            return Ok(response);
        } 
        
        
        [HttpPost("send-bulk-mail")]
        [SwaggerOperation(Summary = "Send bulk mail", Description = "Sends email to users")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Sent = true", Type = typeof(EmailResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> SendBulkMail(SendBulkEmailRequest bulkMessageRequest)
        {
            EmailResponse response = await _emailServices.SendBulkMailAsync(bulkMessageRequest);
            return Ok(response);
        }


        [HttpPost("subcribe-newsletter")]
        [SwaggerOperation(Summary = "Subscribe to our newsletter", Description = "Subscribe to our newsletter")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Subscription successful", Type = typeof(EmailResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> SubscribeNewsletterEmailAsync(string email)
        {
            SubscriptionResponse response = await _emailServices.SubscribeNewsletterEmailAsync(email);
            return Ok(response);
        }

        [HttpGet("newsletter-users")]
        [SwaggerOperation(Summary = "get all Subscribed users", Description = "get all subscribed users")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Users fetched", Type = typeof(EmailResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No user found", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllSubscribedEmailAsync()
        {
            IEnumerable<FetchSubcribedUserEmailResponse> response = await _emailServices.GetAllSubscribedEmailAsync();
            return Ok(response);
        }

        [HttpGet("newsletter-user")]
        [SwaggerOperation(Summary = "get subscribed user", Description = "get subscribed user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "User fetched", Type = typeof(EmailResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No user found", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetSubscribedEmailAsync(string email)
        {
            FetchSubcribedUserEmailResponse response = await _emailServices.GetSubscribedEmailAsync(email);
            return Ok(response);
        }

    }
}
