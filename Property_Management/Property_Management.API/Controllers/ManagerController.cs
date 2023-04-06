using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Property_Management.API.Controllers
{ 
    [Authorize(Roles = "manager")]
    [Route("api/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerServices _managerServices;
        public ManagerController(IManagerServices managerServices)
        {
            _managerServices = managerServices;
        }


        [HttpPost("add-property")]
        [SwaggerOperation(Summary = "Adds a  property")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Property added successfully", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The request may be missing required fields or contain invalid data", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> AddProperty(AddOrUpdatePropertyRequest request)
        {
          Response result = await _managerServices.AddProperty(request);
            return Ok(result);
        }


        [HttpDelete("{propertyId}")]
        [SwaggerOperation(Summary = "Delete a single Property")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Property added successfully", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The request may be missing required fields or contain invalid data", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteProperty(string propertyId)
        {
                var response = await _managerServices.DeleteProperty(propertyId);
                return Ok(response);
        }


        [HttpPut("update-property")]
        [SwaggerOperation(Summary = "Update a property")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "property added successfully", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The request may be missing required fields or contain invalid data", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateProperty(string propertyId, AddOrUpdatePropertyRequest request)
        {
                var response = await _managerServices.UpdateProperty(propertyId, request);
                return Ok(response);
        }


        [HttpGet("Get-all-Properties")]
        [SwaggerOperation(Summary = "Get all properties")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets all properties", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The request may be missing required fields or contain invalid data", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllProperties()
        {
                var response = await _managerServices.GetAllProperties();
                return Ok(response);
        }


        [HttpGet("Get-All-Avaliable-Or-Unavialble-Properties")]
        [SwaggerOperation(Summary = "Get all Available or Unrented Properties")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Adds a property.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The request may be missing required fields or contain invalid data", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task <IActionResult> GetAllAvaliableOrUnavialbleProperties(bool isAvailable)
        {
            var response = await _managerServices.GetAllAvaliableOrUnavialbleProperties(isAvailable);
            return Ok(response);
        }



        [HttpGet("Get-All-Rented-Or-Non-Rented-Properties-By-LandLordID")]
        [SwaggerOperation(Summary = "Get all Rented and non Rented Properties by LandLordID")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Adds a property.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The request may be missing required fields or contain invalid data", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllRentedOrNonRentedPropertiesByLandord(string landlordId, bool condition)
        {
            var response = await _managerServices.GetAllRentedOrNonRentedPropertiesByLandord(landlordId, condition);
            return Ok(response);
        }

    }
}
