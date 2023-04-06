//using Microsoft.AspNetCore.Mvc;
//using Property_Management.BLL.Implementations;
//using Property_Management.BLL.Interfaces;
//using Property_Management.BLL.Models;


//namespace Property_Management.API.Controllers
//{
//    [Route("api/tenant")]
//    [ApiController]
//    public class TenantController : ControllerBase
//    {

//        private readonly ITenantServices _tenantService;
//        private readonly IMaintenanceRequestServices _maintenaceService;

//        public TenantController(ITenantServices tenantService, MaintenanceRequestServices _maintenaceService)
//        {
//            _tenantService = tenantService;
//            this._maintenaceService = _maintenaceService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetTenantAndMaintainance()
//        {
//            var model = await _tenantService.GetTenantsWithMaintenanceAsync();
//            return Ok(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddOrUpdateMaintainance(AddOrUpdateMaintenanceVM model)
//        {
          

//            return BadRequest();
//        }
//    }
//}