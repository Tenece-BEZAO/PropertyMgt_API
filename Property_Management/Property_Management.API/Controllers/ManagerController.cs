using Azure.Core;
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
        private readonly IMangerServices _landLordServices;
        

        public ManagerController(IMangerServices landLordServices)
        {
            _landLordServices = landLordServices;
        }

        [HttpPost("add-property")]
        public async Task<IActionResult> AddProperty(AddPropertyRequest request)
        {
          Response result = await _landLordServices.AddProperty(request);
            return Ok(result);
        }


        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> DeleteProperty(DeletePropertyRequest request)
        {
                var response = await _landLordServices.DeleteProperty(request);
                return Ok(response);
        }


        //}  [HttpDelete("propertyId")]
        //public async Task<IActionResult> DeleteProperty(string propertyId)
        //{
        //    try
        //    {
        //        var response = await _landLordServices.DeleteProperty(propertyId);
        //        return Ok(response);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return NotFound(ex);
        //    }
        //    catch (Exception)
        //    {
        //        StatusCode(500, "An error occurred while deleting the property.");
        //    }

        //}
    }
}
