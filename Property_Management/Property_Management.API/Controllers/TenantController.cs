using Microsoft.AspNetCore.Mvc;
using Property_Management.API.Controllers;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Models;
using Property_Management.DAL.Context;
using Property_Management.DAL.Entities;


namespace Property_Management.API.Controllers
{
    /*[AutoValidateAntiforgeryToken]*/
    [Route("api/tenant")]
    [ApiController]
    public class TenantController : ControllerBase
    {

        private readonly ITenantServices _tenantService;
        private readonly IMaintenanceRequestServices _maintenaceService;

        public TenantController(ITenantServices tenantService, IMaintenanceRequestServices _maintenaceService)
        {
            _tenantService  = tenantService;
            this._maintenaceService = _maintenaceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTenantAndMaintainance()
        {
            var model = await _tenantService.GetTenantsWithMaintenanceAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateMaintainance(AddOrUpdateMaintenanceVM model)
        {
            if (ModelState.IsValid)
            {

                return Ok(await _maintenaceService.AddOrUpdateAsync(model));

            }
            return BadRequest();
        }


        // [HttpGet("{userid}/{taskId}")]
        /*public async Task<IActionResult> Delete(int userId, int taskId)
        {
            var (success, msg) = await _todoService.DeleteAsync(userId, taskId);

            if (success)
            {
                TempData["SuccessMsg"] = msg;
                return RedirectToAction("Index");
            }

            TempData["ErrMsg"] = msg;
            return RedirectToAction("Index");
        }*/
    }
}


