using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;

namespace Property_Management.API.Controllers
{
    [Route("api/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IMangerServices _managerServices;
        

        public ManagerController(IMangerServices landLordServices)
        {
            _managerServices = landLordServices;
        }

        [HttpPost("add-property")]
        public async Task<IActionResult> AddProperty(AddPropertyRequest request)
        {
          Response result = await _managerServices.AddProperty(request);
            return Ok(result);
        }


        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> DeleteProperty(DeletePropertyRequest request)
        {
                var response = await _managerServices.DeleteProperty(request);
                return Ok(response);
        }
    }
}
