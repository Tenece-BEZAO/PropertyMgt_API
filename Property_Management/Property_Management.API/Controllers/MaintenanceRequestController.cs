using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Property_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceRequestController : ControllerBase
    {
        private readonly IMaintenanceRequestServices _maintenanceRequestServices;
        public MaintenanceRequestController (IMaintenanceRequestServices maintenanceRequestServices)
        {
            _maintenanceRequestServices= maintenanceRequestServices;
        }
        [HttpPost ("Create_Maintenance-Request")]
        public async Task<IActionResult> CreateMaintenanceRequestAsync(MaintenanceRequestrequests requests)
        {
            Response result = await _maintenanceRequestServices.CreateMaintenanceRequestAsync(requests);
            return Ok(result);
        }
        

    }
}
