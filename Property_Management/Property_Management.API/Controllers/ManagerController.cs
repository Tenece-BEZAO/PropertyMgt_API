using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;

namespace Property_Management.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<IActionResult> AddProperty(AddOrUpdatePropertyRequest request)
        {
          Response result = await _managerServices.AddProperty(request);
            return Ok(result);
        }


        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> DeleteProperty(string propertyId)
        {
                var response = await _managerServices.DeleteProperty(propertyId);
                return Ok(response);
        }

        [HttpPut("update-property")]
        public async Task<IActionResult> UpdateProperty(string propertyId, AddOrUpdatePropertyRequest request)
        {
                var response = await _managerServices.UpdateProperty(propertyId, request);
                return Ok(response);
        }


        [HttpGet("Get-all-Properties")]
        public async Task<IActionResult> GetAllProperties()
        {
                var response = await _managerServices.GetAllProperties();
                return Ok(response);
        }

    }
}
