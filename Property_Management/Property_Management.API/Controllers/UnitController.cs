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
    [Route("api/unit")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitServices _unitServices;
        public UnitController(IUnitServices unitServices)
        {
            _unitServices = unitServices;
        }

        [Authorize(Roles = "manager, admin, landlord")]
        [HttpPost("new-unit")]
        [SwaggerOperation(Summary = "Create a new unit for a property", Description = "Create new unit.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "added = true")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Sorry something went wrong while trying to create the unit. Do try again.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateNewUnit(NewUnitRequest request)
        {
            bool response = await _unitServices.CreateUnitAsync(request);
            return Ok(new { added = response });
        }

        [Authorize(Roles = "manager, admin, landlord")]
        [HttpDelete("delete-unit")]
        [SwaggerOperation(Summary = "Delete a unit for a property", Description = "Delete unit.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "deleted = true")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Unit with this Id was not found.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteUnit(string unitId)
        {
            bool response = await _unitServices.DeleteUnitAsync(unitId);
            return Ok(new { deleted = response });
        }

        [Authorize]
        [HttpGet("get-unit")]
        [SwaggerOperation(Summary = "get a single unit for a property", Description = "get unit.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Returns a single object of {UnitResponse}", Type = typeof(UnitResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Unit with this Id was not found.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetUnit(string unitId)
        {
            UnitResponse response = await _unitServices.GetUnitAsync(unitId);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("units")]
        [SwaggerOperation(Summary = "get a single unit for a property", Description = "get unit.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Returns list of all available units", Type = typeof(UnitResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Unit with this Id was not found.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetUnits([FromQuery] RequestParameters requestParam)
        {
            IEnumerable<UnitResponse> response = await _unitServices.GetUnitsAsync(requestParam);
            return Ok(response);
        }

        [Authorize(Roles ="admin, manager, landlord")]
        [HttpPut("update-unit")]
        [SwaggerOperation(Summary = "Update a single unit for a property", Description = "Update unit.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Update a single unit", Type = typeof(UnitResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Unit with this Id was not found.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateUnit(NewUnitRequest request)
        {
            UnitResponse response = await _unitServices.UpdateUnitAsync(request);
            return Ok(response);
        }
    }
}
