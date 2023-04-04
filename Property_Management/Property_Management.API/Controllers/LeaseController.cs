using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Property_Management.API.Controllers
{
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
        public async Task<IActionResult> CreateNewLease(CreateLeaseRequest request)
        {
            Response result = await _leaseServices.CreateLease(request);
            return Ok(result);
        }
        
        [HttpDelete("remove-lease", Name = "Remove Lease")]
        [SwaggerOperation(Summary = "Remove lease", Description = "Remove lease")]
        public async Task<IActionResult> RemoveLease(string leaseId)
        {
            Response result = await _leaseServices.RemoveLease(leaseId);
            return Ok(result);
        }
        
        
        [HttpPut("update-lease", Name = "Update Lease")]
        [SwaggerOperation(Summary = "Update lease", Description = "Update lease")]
        public async Task<IActionResult> UpdateLease(string leaseId, CreateLeaseRequest request)
        {
            Response result = await _leaseServices.UpdateLease(leaseId, request);
            return Ok(result);
        }
        
        [HttpPut("toggle-status", Name = "Accept or Reject Lease")]
        [SwaggerOperation(Summary = "Accept of reject lease", Description = "Accept of reject lease")]
        public async Task<IActionResult> AcceptOrRejectLease(AcceptLeaseRequest request)
        {
            Response result = await _leaseServices.AcceptOrRejectLease(request);
            return Ok(result);
        }

        [HttpGet("get-payment-details")]
        public async Task<IActionResult> GetPaymentDetails()
        {
            var lease = await _leaseServices.GetAllRentPaymentDetails();
            return Ok(lease);
        }
        
        [HttpGet("get-tenant-payment-detail")]
        public async Task<IActionResult> GetPamentDetail(string tenantId)
        {
            var lease = await _leaseServices.GetRentPaymentDetails(tenantId);
            return Ok(lease);
        }


        [HttpGet("Rent-expiration-alert.")]
        public async Task<IActionResult> GetTentantRentPaymentDetail(string tenantId)
        {
            var response = await _leaseServices.NofityRentExiration(tenantId);
            return Ok(response);
        }

        [HttpGet("get-upto-date-tenant")]
        public async Task<IActionResult> GetTenantWhosPaymentDetailsAreStillUpToDate()
        {
            var response = await _leaseServices.GetTenantWhosPaymentDetailsAreStillUpToDate();
            return Ok(response);
        }
    }
}
