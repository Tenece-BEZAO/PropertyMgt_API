using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class MaintenanceServices : IMaintenanceServices
    {
        private readonly IRepository<MaintenanceRequest> _mrRepo;
        private readonly IRepository<Tenant> _tenantRepo;
        private readonly IRepository<Unit> _unitRepo;
        private readonly IRepository<Staff> _staffRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MaintenanceServices(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mrRepo = _unitOfWork.GetRepository<MaintenanceRequest>();
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
            _unitRepo = _unitOfWork.GetRepository<Unit>();
            _staffRepo = _unitOfWork.GetRepository<Staff>();
        }
        public async Task<bool> AddMaintenanceAsync(AddMaintenanceRequest request)
        {
            if (request == null) throw new InvalidOperationException("Request emailBody cannot be empty.");

            bool isUnitxist = await _unitRepo.AnyAsync(u => u.UnitId == request.UnitId);
            if (!isUnitxist) throw new InvalidOperationException($"Unit with the Id {request.UnitId} does not exist.");
            bool isTenantExist = await _tenantRepo.AnyAsync(t => t.TenantId == request.RequestedBy);
            if (!isTenantExist) throw new InvalidOperationException($"Tenant with the id {request.RequestedBy} was not found.");
            bool isStaffExist = await _staffRepo.AnyAsync(s => s.StaffId == request.ReportedTo);
            if (!isStaffExist) throw new InvalidOperationException($"Staff with the id {request.ReportedTo} was not found.");

            request.Id = Guid.NewGuid().ToString();
            MaintenanceRequest newMR = _mapper.Map<MaintenanceRequest>(request);
            MaintenanceRequest response = await _mrRepo.AddAsync(newMR);
            if (response == null)
                throw new InvalidOperationException("Sorry something went wrong while trying to create the unit. Do try again.");
            return true;
        }

        public async Task<bool> DeleteRequestAsync(string mrId)
        {
            MaintenanceRequest mR = await _mrRepo.GetByIdAsync(mrId);
            if (mR == null) throw new InvalidOperationException($"Maintenance Request with the Id {mrId} does not exist.");
            mR.IsDeleted = true;
            await _mrRepo.UpdateAsync(mR);
            return true;
        }

        public async Task<IEnumerable<MrResponse>> GetAllRequestAsync()
        {
            IEnumerable<MaintenanceRequest> mrTenant = (await _mrRepo.GetByAsync(
                include: mr => mr.Include(m => m.Unit).ThenInclude(p => p.Tenants).ThenInclude(u => u.Property)));
            if (!mrTenant.Any()) throw new InvalidOperationException("Maintenance request was empty.");

            IEnumerable<MaintenanceRequest> mrStaff = (await _mrRepo.GetByAsync(predicate: mr => mr.ReportedTo != null, include: mr => mr.Include(m => m.Employee)));
            if (!mrStaff.Any()) throw new InvalidOperationException("No Employee found.");

            return mrTenant.Join(mrStaff, mrT => mrT.Id, mrS => mrS.Id, (mr, sMr) =>
                     new MrResponse
                     {
                         Description = mr.Description,
                         Tenant = new TenantMrResponse
                         {
                            TenantName = $"{mr?.Tenant?.FirstName} {mr?.Tenant?.LastName}",
                            Property = new PropertyResponse
                            {
                                Name = mr?.Tenant.Unit.Property.Name,
                                Price = mr.Tenant.Unit.Property.Price,
                                Status = mr.Tenant.Unit.Property.Status,
                                Image = mr?.Tenant.Unit.Property.Image,
                                PropertyId = mr?.Tenant.Unit.Property.PropertyId,
                                LandLordId = mr?.Tenant.Unit.Property.LandLordId,
                                Unit = new UnitResponse
                                {
                                    UnitId = mr?.Tenant.Unit?.UnitId,
                                    UnitDescription = mr?.Tenant?.Unit?.Description,
                                    UnitName = mr?.Tenant?.Unit?.Name,
                                    UnitType = mr?.Tenant?.Unit?.UnitType.GetStringValue(),
                                    NumberOfRooms = mr.Tenant.Unit.NumOfBedRooms
                                },
                            },
                         },
                         Staff = new StaffMrResponse
                         {
                             StaffName = $"{sMr?.Employee?.FirstName} {sMr?.Employee?.LastName}",
                             Available = sMr.Employee.Available,
                             Occupation = sMr?.Employee?.Occupation
                         },
                         DateLogged = mr?.DateLogged.ToLongDateString(),
                         DueDate = mr?.DueDate.ToLongDateString(),
                         Priority = mr?.Priority.GetStringValue(),
                         Status = mr.Status.GetStringValue(),
                     }

              );
        }

        public async Task<MrResponse> GetRequestAsync(string mrId)
        {
            bool isMrExist = await _mrRepo.AnyAsync(mr => mr.Id == mrId);
            if (!isMrExist) throw new InvalidOperationException("No maintenance request with this Id exist.");

            MaintenanceRequest MR = (await _mrRepo.GetSingleByAsync(
                predicate: mr => mr.Id == mrId && mr.RequestedBy != null && mr.Tenant.Property.Unit != null && mr.Tenant.Property != null,
               include: mr => mr.Include(m => m.Tenant).ThenInclude(p => p.Property).ThenInclude(u => u.Unit)));
            if (MR == null) throw new InvalidOperationException("No Maintenance request found.");
            Staff staff = await _staffRepo.GetSingleByAsync(s => s.StaffId == MR.ReportedTo);
            if (staff == null) throw new InvalidOperationException("Error! staff not found.");

            return new MrResponse
            {
                Description = MR.Description,
                Tenant = new TenantMrResponse
                {
                    TenantName = $"{MR?.Tenant?.FirstName} {MR?.Tenant?.LastName}",
                    Property = new PropertyResponse
                    {
                        Name =  MR?.Tenant.Property.Name,
                        Price = MR.Tenant.Property.Price,
                        Status = MR.Tenant.Property.Status,
                        Image = MR?.Tenant.Property.Image,
                        PropertyId = MR?.Tenant.Property.PropertyId,
                        LandLordId = MR?.Tenant.Property.LandLordId,
                        Unit = new UnitResponse
                        {
                            UnitId = MR?.Tenant.Property.Unit?.UnitId,
                            UnitDescription = MR?.Tenant?.Property?.Unit?.Description,
                            UnitName = MR?.Tenant?.Property?.Unit?.Name,
                            UnitType = MR?.Tenant?.Property.Unit?.UnitType.GetStringValue(),
                            NumberOfRooms = MR.Tenant.Property.Unit.NumOfBedRooms
                        },
                    },
                },

                Staff = new StaffMrResponse
                {
                    StaffName = $"{staff.FirstName} {staff.LastName}",
                    Available = staff.Available,
                    Occupation = staff.Occupation
                },
                DateLogged = MR.DateLogged.ToLongDateString(),
                DueDate = MR.DueDate.ToLongDateString(),
                Priority = MR.Priority.GetStringValue(),
                Status = MR.Status.GetStringValue(),
            };
        }

        public async Task<bool> UpdateRequestAsync(UpdateMaintenanceRequest request)
        {
            bool isMrExist = await _mrRepo.AnyAsync(mr => mr.Id == request.Id);
            if (!isMrExist) throw new InvalidOperationException("Maintenance request with the Id provided was not found.");

            MaintenanceRequest newMR = _mapper.Map<MaintenanceRequest>(request);
            MaintenanceRequest response = await _mrRepo.UpdateAsync(newMR);
            if (response == null)
                throw new InvalidOperationException("Sorry something went wrong while trying to create the unit. Do try again.");
            return true;
        }
    }
}
