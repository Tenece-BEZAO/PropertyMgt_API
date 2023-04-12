using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using Property_Management.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Property_Management.API.Controllers
{
   
    [Authorize(Roles = "Manager")]
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
            if (result == null)
                return BadRequest();

            return Ok(result);
        }



/*
        [HttpGet]
        [Route("get-tenant-by-id")]
        public async Task<IActionResult> GetTenantById(string id)
        {
            var result = await _tenantService.GetTenantById(id);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }*/
        [HttpGet]
        [Route("get-tenant-by-id")]
        [SwaggerOperation(Summary = "Fetch a Tenanat by id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "This route fetches a tenant in the database using the id", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to fetch tenant by id", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Response))]
        public async Task<ActionResult<TenantDTO>> GetTenantById(string id)
        {
            var tenant = await _tenantService.GetTenantById(id);

            if (tenant == null)
            {
                return NotFound();
            }

            return tenant;
        }

        /* [HttpPost]
         [Route("create-tenant")]
         public async Task<IActionResult> CreateTenant([FromBody] TenantDTO tenant)
         {
             var result = await _tenantService.CreateTenant(tenant);
             if (result == null)
                 return BadRequest();


             return Ok(result);
         }*/
        [HttpPost]
        [Route("create-tenant")]
        [SwaggerOperation(Summary = "creates a Tenanat using userId")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "This route creates a tenant in the database using userId", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create tenant", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Response))]
        public async Task<ActionResult<int>> CreateTenant(TenantDTO tenantDto)
        {
            var tenantId = await _tenantService.CreateTenant(tenantDto);

            return CreatedAtAction(nameof(GetAllTenants), new
            {
                id = tenantId
            }, tenantId);
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
            if (!result)
            {
                return BadRequest();
            }

            return Ok("Tenant was deleted successfully");
        }



        [HttpPut]
        [Route("update-Tenant")]
        [SwaggerOperation(Summary = "Updates a Tenanat by id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "This route updates a tenant in the database using the id", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to update a tenant by id", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(Response))]
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