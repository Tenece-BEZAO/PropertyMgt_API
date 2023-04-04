using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Models;

 
namespace Property_Management.API.Controllers
{
    [Route("api/tenant")]
    [ApiController]
    public class TenantController : ControllerBase
    {

        private readonly ITenantServices _tenantService;
        private readonly IMapper _mapper;


        public TenantController(ITenantServices tenantService, IMapper mapper)
        {
            _tenantService = tenantService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantDto>>> GetTenants()
        {
            var tenants = _tenantService.GetAllTenantsAsync(includeProperties: "Leases.LeasePayments, Leases.Unit");

            var tenantDtos = _mapper.Map<IEnumerable<TenantDto>>(tenants);

            return Ok(tenantDtos);
        }
        /*public async Task<IActionResult> GetTenantAndMaintainance()
        {
            var model = await _tenantService.GetTenantsWithMaintenanceAsync();
            return Ok(model);
        }*/

        /*[HttpPost]
        public async Task<IActionResult> AddOrUpdateMaintainance(AddOrUpdateMaintenanceVM model)
        {
            if (ModelState.IsValid)
            {

                return Ok(await _maintenaceService.AddOrUpdateAsync(model));

            }
            return BadRequest();
        }*/
    }
}