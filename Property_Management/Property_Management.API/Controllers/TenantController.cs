using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Property_Management.API.Controllers
{
    /*[Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantServices _tenantServices;

        public TenantController(ITenantServices tenantServices)
        {
            _tenantServices = tenantServices;
        }

        [HttpPost("new-tenant")]
        public async Task<IActionResult> CreateNewTenant(CreateTenantRequest request)
        {
            Response result = await _tenantServices.CreateTenant(request);
            return Ok(result);
        }

        [HttpDelete("remove-tenant", Name = "Remove Tenant")]
        [SwaggerOperation(Summary = "Remove lease", Description = "Remove Tenant")]
        public async Task<IActionResult> RemoveTenant(string tenantId)
        {
            Response result = await _tenantServices.RemoveTenant(tenantId);
            return Ok(result);
        }


        [HttpPut("update-tenant", Name = "Update Tenant")]
        [SwaggerOperation(Summary = "Update Tenant", Description = "Update Tenant")]
        public async Task<IActionResult> UpdateTenant(string tenantId, CreateTenantRequest request)
        {
            Response result = await _tenantServices.UpdateTenant(tenantId, request);
            return Ok(result);
        }

        *//*[HttpPut("toggle-status", Name = "Accept or Reject Lease")]
        [SwaggerOperation(Summary = "Accept of reject tenant", Description = "Accept of reject lease")]
        public async Task<IActionResult> AcceptOrRejectLease(AcceptTenantRequest request)
        {
            Response result = await _tenantServices.AcceptOrRejectTenant(request);
            return Ok(result);
        }*//*

        [HttpGet("get-payment-details")]
        public async Task<IActionResult> GetPaymentDetails()
        {
            var lease = await _tenantServices.GetAllRentPaymentDetails();
            return Ok(lease);
        }

        [HttpGet("get-tenant-payment-detail")]
        public async Task<IActionResult> GetPamentDetail(string tenantId)
        {
            var lease = await _tenantServices.GetRentPaymentDetails(tenantId);
            return Ok(lease);
        }


        [HttpGet("Rent-expiration-alert.")]
        public async Task<IActionResult> GetTentantRentPaymentDetail(string tenantId)
        {
            var response = await _tenantServices.NofityRentExiration(tenantId);
            return Ok(response);
        }

        [HttpGet("get-upto-date-tenant")]
        public async Task<IActionResult> GetTenantWhosPaymentDetailsAreStillUpToDate()
        {
            var response = await _tenantServices.GetTenantWhosPaymentDetailsAreStillUpToDate();
            return Ok(response);*/

    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {

        private readonly ITenantServices _tenantService;

        public TenantController(ITenantServices tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        [Route("get-all-tenants")]
        public async Task<IActionResult> GetAllTenants()
        {
            var result = await _tenantService.GetAllTenants();
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet]
        [Route("get-tenant-by-id")]
        public async Task<IActionResult> GetTenantById(string id)
        {
            var result = await _tenantService.GetTenantById(id);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        [Route("create-tenant")]
        public async Task<IActionResult> CreateTenant([FromBody] TenantDTO tenant)
        {
            var result = await _tenantService.CreateTenant(tenant);
            if (result == null)
                return BadRequest();


            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-tenant")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var result = await _tenantService.DeleteTenant(id);
            if (!result)
            {
                return BadRequest();
            }

            return Ok("Tenant was deleted successfully");
        }



        [HttpPut]
        [Route("update-Tenant")]
        public async Task<IActionResult> UpdateTenant( string id, TenantDTO tenantDto)
        {
            
            if (id != tenantDto.UserId)
            {
                return BadRequest();
            }

            var result = await _tenantService.EditTenant(id, tenantDto);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }



}