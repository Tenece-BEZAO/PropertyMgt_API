using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;


namespace Property_Management.BLL.Implementations
{
    public class MaintenaceRequestServices : IMaintenanceRequestServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Tenant> _tenantRepo;
        private readonly IRepository<MaintenanceRequest> _maintenaceRequestRepo;

        public MaintenaceRequestServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _maintenaceRequestRepo = _unitOfWork.GetRepository<MaintenanceRequest>();
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
        }

        public async Task<Response> AddOrUpdateAsync(AddOrUpdateMaintenanceVM model)
        {

            // _taskRepo.GetSingleByAsync(t => t.UserId == model.UserId && t.Id == model.TaskId);

            Tenant tenant = await _tenantRepo.GetSingleByAsync(u => u.TenantId == model.TenantId, include: u => u.Include(x => x.MaintenanceRequests), tracking: true);

            if (tenant == null)
            {
                return new Response
                {
                    StatusCode = 400,
                    Message = $"tenant with id:{model.TenantId} wasn't found",
                    Action = $"Maintainace request {false}"
                };
            }

            MaintenanceRequest maintenance = tenant.MaintenanceRequests.SingleOrDefault(t => t.MaintenanceRequestId == model.MaintenanceRequestId);


            if (maintenance != null)
            {

                _mapper.Map(model, maintenance);

                //
                // task.Title = model.Title;
                // task.Description = model.Description;
                // task.Priority = (model.Priority ?? Priority.Normal);
                // task.DueDate = model.DueDate;

                await _unitOfWork.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 400,
                    Message = $"tenant with id:{model.TenantId} has been updated.",
                    Action = $"Maintainace request {true}"
                };
            }

            // var newTask = _mapper.Map<AddOrUpdateTaskVM,Todo>(model);
            var newMaintenaceRequest = _mapper.Map<MaintenanceRequest>(model);

            // var newTask = new Todo
            // {
            //  
            //     Title = model.Title,
            //     Description = model.Description,
            //     Priority = model.Priority ?? Priority.Normal,
            //     DueDate = model.DueDate,
            //
            // };
            tenant.MaintenanceRequests.Add(newMaintenaceRequest);

            var rowChanges = await _unitOfWork.SaveChangesAsync();

            return rowChanges > 0 ? new Response
            {
                StatusCode = 400,
                Message = $"tenant with id:{model.TenantId} wasn't found",
                Action = $"Maintainace request {false}"
            } : new Response
            {
                StatusCode = 400,
                Message = $"tenant with id:{model.TenantId} wasn't found",
                Action = $"Maintainace request {false}"
            };

        }


    }
}