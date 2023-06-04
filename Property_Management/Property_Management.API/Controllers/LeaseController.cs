using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Property_Management.API.Controllers
{
    [Authorize(Roles = "landlord")]
    [Route("api/lease")]
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

    }
}
