using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Property_Management.API.Controllers
{
    [Route("api/[controller]")]
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
            EmailResponse reponse = await _emailServices.SendMailAsync(emailRequest);
            return Ok(reponse);
        } 
        
        
        [HttpPost("send-bulk-mail")]
        [SwaggerOperation(Summary = "Send bulk mail", Description = "Sends email to users")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Sent = true", Type = typeof(EmailResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> SendBulkMail(SendBulkEmailRequest bulkMessageRequest)
        {
            EmailResponse reponse = await _emailServices.SendBulkMailAsync(bulkMessageRequest);
            return Ok(reponse);
        }
    }
}
