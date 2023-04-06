using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Property_Management.API.Controllers
{
    [Authorize(Roles = "landlord")]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaseController : ControllerBase
    {
        private readonly ILeaseServices _leaseServices;

        public LeaseController(ILeaseServices leaseServices)
        {
            _leaseServices = leaseServices;
        }

        [HttpPost("new-lease")]
        [SwaggerOperation(Summary = "Create new lease", Description = "Create new lease")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Your lease has been created.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Tenant with the id {request.TenantId} was not found.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateNewLease(CreateLeaseRequest request)
        {
            Response result = await _leaseServices.CreateLease(request);
            return Ok(result);
        }
        
        [HttpDelete("remove-lease", Name = "Remove Lease")]
        [SwaggerOperation(Summary = "Remove lease", Description = "Remove lease")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Lease with the ID {leaseId} was deleted.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Lease with the ID {leaseId} was not found.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RemoveLease(string leaseId)
        {
            Response result = await _leaseServices.RemoveLease(leaseId);
            return Ok(result);
        }
        
        
        [HttpPut("update-lease", Name = "Update Lease")]
        [SwaggerOperation(Summary = "Update lease", Description = "Update lease")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Lease with id {leaseId} has been updated.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The update failed. try again.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateLease(string leaseId, CreateLeaseRequest request)
        {
            Response result = await _leaseServices.UpdateLease(leaseId, request);
            return Ok(result);
        }
        
        [HttpPut("toggle-status", Name = "Accept or Reject Lease")]
        [SwaggerOperation(Summary = "Accept of reject lease", Description = "Accept of reject lease")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Your have accepted this lease.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "lease with the Id {request.LeaseId} and property Id {request.PropertyId} was not found.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> AcceptOrRejectLease(AcceptLeaseRequest request)
        {
            Response result = await _leaseServices.AcceptOrRejectLease(request);
            return Ok(result);
        }

        [HttpGet("get-payment-details", Name = "get-all-payment-details.")]
        [SwaggerOperation(Summary = "Get tenants rent details.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "", Type = typeof(IEnumerable<Tenant>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No payment was fetched.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetPaymentDetails()
        {
            var lease = await _leaseServices.GetAllRentPaymentDetails();
            return Ok(lease);
        }
        
        [HttpGet("get-tenant-payment-detail")]
        [SwaggerOperation(Summary = "Get tenants rent details.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "", Type = typeof(IEnumerable<Tenant>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No tenant was found", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetPamentDetail(string tenantId)
        {
            IEnumerable<Tenant> lease = await _leaseServices.GetRentPaymentDetails(tenantId);
            return Ok(lease);
        }


        [HttpGet("Rent-expiration-check", Name = "check-if-rent-expired.")]
        [SwaggerOperation(Summary = "Checks if tenant rent has expired and return a message.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Your rent has expired.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Your rent is still active.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No tenant was found", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetTentantRentPaymentDetail(string tenantId)
        {
            Response response = await _leaseServices.NofityRentExiration(tenantId);
            return Ok(response);
        }

        [HttpGet("get-upto-date-tenant", Name = "get unexpired rent tenants.")]
        [SwaggerOperation(Summary = "fetches tenanats whos rent has not expired.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "fetches user whos rent is up to date.", Type = typeof(IEnumerable<PaymentInfoResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No tenant was found", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetTenantWhosPaymentDetailsAreStillUpToDate()
        {
            IEnumerable<PaymentInfoResponse> response = await _leaseServices.GetTenantWhosPaymentDetailsAreStillUpToDate();
            return Ok(response);
        }
    }
}
