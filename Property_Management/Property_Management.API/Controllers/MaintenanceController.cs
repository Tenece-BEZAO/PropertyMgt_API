using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Property_Management.API.Controllers
{
    [Route("api/maintenance")]
    [ApiController]
    [Authorize]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceServices _maintenanceServices;
        public MaintenanceController(IMaintenanceServices maintenanceServices)
        {
            _maintenanceServices = maintenanceServices;
        }

        [HttpPost("create-mr")]
        [SwaggerOperation(Summary = "Creates new maintenance request", Description = "Creates new maintenance request")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "created = true")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Sorry something went wrong while trying to create the unit. Do try again.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateMr(AddMaintenanceRequest request)
        {
            bool response = await _maintenanceServices.AddMaintenanceAsync(request);
            return Ok(new { created = response });
        }

        [HttpDelete("delete-mr")]
        [SwaggerOperation(Summary = "Deletes maintenance request", Description = "Deletes maintenance request")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "deleted = true")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Unit with the Id {mrId} does not exist.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteMr(string mrId)
        {
            bool response = await _maintenanceServices.DeleteRequestAsync(mrId);
            return Ok(new {deleted = response});
        }

        [HttpGet("get-mr")]
        [SwaggerOperation(Summary = "get a single maintenance request", Description = "get a single maintenance request")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Returns an object containing a single maintenance request",Type = typeof(MrResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Unit with the Id {mrId} does not exist.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetMr(string mrId)
        {
            MrResponse response = await _maintenanceServices.GetRequestAsync(mrId);
            return Ok(response);
        }
        
        [HttpGet("get-all-mr")]
        [SwaggerOperation(Summary = "fetches all maintenance request", Description = "Fetches all maintenance request")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Returns an object containing list of maintenance request", Type = typeof(IEnumerable<MrResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Maintenance request was empty.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllMr()
        {
            IEnumerable<MrResponse> response = await _maintenanceServices.GetAllRequestAsync();
            return Ok(response);
        } 
        
        [HttpPut("update-mr")]
        [SwaggerOperation(Summary = "Updates maintenance request", Description = "Updates maintenance request")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "updated = true")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "maintenance request with the Id {mrId} does not exist.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateMr(UpdateMaintenanceRequest request)
        {
            bool response = await _maintenanceServices.UpdateRequestAsync(request);
            return Ok(new { updated = response });
        }
    }
}
