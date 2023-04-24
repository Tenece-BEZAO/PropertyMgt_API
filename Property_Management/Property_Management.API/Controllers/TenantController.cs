using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Implementations;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Property_Management.API.Controllers
{

    [Authorize(Roles = "manager")]
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
        [SwaggerOperation(Summary = "Fetch all Tenanats")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "This route fetches all the tenants in the database", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to fetch tenants", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Response))]
        public async Task<IActionResult> GetAllTenants()
        {
            var result = await _tenantService.GetAllTenants();
            return Ok(result);
        }

        [HttpGet]
        [Route("get-tenant-by-id")]
        [SwaggerOperation(Summary = "Fetch a Tenanat by id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "This route fetches a tenant in the database using the id", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to fetch tenant by id", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Response))]
        public async Task<ActionResult<TenantDTO>> GetTenantById(string id)
        {
            var tenant = await _tenantService.GetTenantById(id);
                return Ok(tenant);
        }

        [HttpPost]
        [Route("create-tenant")]
        [SwaggerOperation(Summary = "creates a Tenanat using userId")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "This route creates a tenant in the database using userId", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create tenant", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Response))]
        public async Task<ActionResult<int>> CreateTenant(UserRegistrationRequest request)
        {
            var tenantId = await _tenantService.CreateTenant(request);
            return Ok(tenantId);
        }
        [HttpDelete]
        [Route("delete-tenant")]
        [SwaggerOperation(Summary = "Deletes a Tenanat by id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "This route deletes a tenant in the database using the id", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to delete a tenant by id", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Response))]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var result = await _tenantService.DeleteTenant(id);
            return Ok(result);
        }



        [HttpPut]
        [Route("update-Tenant")]
        [SwaggerOperation(Summary = "Updates a Tenanat by id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "This route updates a tenant in the database using the id", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to update a tenant by id", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Response))]
        public async Task<IActionResult> UpdateTenant(TenantDTO tenantDto)
        {
            
            var result = await _tenantService.UpdateTenant(tenantDto);
            return Ok(result);
        }

        [HttpGet("get-payment-details", Name = "get-all-payment-details.")]
        [SwaggerOperation(Summary = "Get tenants rent details.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "", Type = typeof(IEnumerable<PaymentInfoResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No payment was fetched.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetPaymentDetails()
        {
            IEnumerable<PaymentInfoResponse> lease = await _tenantService.GetAllRentPaymentDetails();
            return Ok(lease);
        }

        [HttpGet("get-tenant-payment-detail")]
        [SwaggerOperation(Summary = "Get tenants rent details.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "", Type = typeof(IEnumerable<PaymentInfoResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No tenant was found", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetPamentDetail(string tenantId)
        {
            IEnumerable<Tenant> lease = await _tenantService.GetRentPaymentDetails(tenantId);
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
            Response response = await _tenantService.NofityRentExiration(tenantId);
            return Ok(response);
        }

        [HttpGet("get-upto-date-tenant", Name = "get unexpired rent tenants.")]
        [SwaggerOperation(Summary = "fetches tenanats whos rent has not expired.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "fetches user whos rent is up to date.", Type = typeof(IEnumerable<PaymentInfoResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No tenant was found", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetTenantWhosPaymentDetailsAreStillUpToDate()
        {
            IEnumerable<PaymentInfoResponse> response = await _tenantService.GetTenantWhosPaymentDetailsAreStillUpToDate();
            return Ok(response);
        }
    }
}

