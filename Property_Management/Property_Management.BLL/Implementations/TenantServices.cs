using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

using Property_Management.DAL.Implementations;


namespace Property_Management.BLL.Implementations
{

    public class TenantServices : ITenantServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Tenant> _tenantRepo;

        public TenantServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
        }
        /*public void Create(CreateTenantVM model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tenant> GetTenants()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TenantWithMaintenaceVM>> GetTenantsWithMaintenanceAsync()
        {

            return (await _tenantRepo.GetAllAsync(include: u => u.Include(t => t.MaintenanceRequests))).Select(u => new TenantWithMaintenaceVM
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Maintenances = u.MaintenanceRequests.Select(t => new MaintenaceVM
                {
                    MaintenanceRequestId = t.MaintenanceRequestId,
                    UnitId = t.UnitId,
                    Description = t.Description,
                    ReportedTo = t.ReportedTo,
                    Priority = t.Priority.ToString(),
                    LoggedBy = t.LoggedBy,
                    DateLogged = t.DateLogged,
                    DueDate = t.DueDate
                })
            }); ;
        }*/


        public async Task<IEnumerable<TenantDto>> GetAllTenantsAsync()
        {
            var tenants =  await _tenantRepo.GetByAsync(include: t=> t.Include(t => t.Leases).ThenInclude(l => l.Payment));

            var tenantDtos = tenants.Select(t => new TenantDto
            {
                Id = t.TenantId,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber,
                Leases = new LeaseDto
                {
                    Id = t.Leases.Id,
                    StartDate = t.Leases.StartDate,
                    EndDate = t.Leases.EndDate,
                    Description = t.Leases.Description,
                    UnitId = t.UnitId,
                    TenantId = t.TenantId,
                    PaymentsDtos = new PaymentDto
                    {
                        Id = t.Payments.Id,
                        PaymentDate = t.Payments.PaymentDate,
                        Amount = t.Payments.Amount,
                        LeaseId = t.Payments.LeaseId
                    },
                },
            });

            return tenantDtos;
        }

        public async Task<TenantDto> GetTenantByIdAsync(int id)
        {
            var tenant = await _tenantRepo.GetByAsync(include: t => t.Include(t => t.Leases).ThenInclude(l => l.Payment);
            if (tenant == null)
            {
                return null;
            }

            var tenantDto = new TenantDto
            {

                Id = tenant.Id,
                FirstName = tenant.FirstName,
                LastName = tenant.LastName,
                Email = tenant.Email,
                PhoneNumber = tenant.PhoneNumber,
                Leases = tenant.Leases.Select(l => new LeaseDto
                {
                    Id = l.Id,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    Description = l.Description,
                    UnitId = l.UnitId,
                    TenantId = l.TenantId,
                    PaymentsDtos = new PaymentDto
                    {
                        Id = l.Payments.Id,
                        PaymentDate = l.Payments.PaymentDate,
                        Amount = l.Payments.Amount,
                        LeaseId = l.Payments.LeaseId
                    },
                })
            };

            return tenantDto;
        }

        public async Task CreateTenantAsync(TenantDto tenantDto)
        {
            var tenant = new Tenant
            {
                FirstName = tenantDto.FirstName,
                LastName = tenantDto.LastName,
                Email = tenantDto.Email,
                PhoneNumber = tenantDto.PhoneNumber
            };

            await _tenantRepo.AddAsync(tenant);
            await _unitOfWork.SaveChangesAsync();

            tenantDto.Id = tenant.TenantId;
        }

        public async Task UpdateTenantAsync(int id, TenantDto tenantDto)
        {
            var tenant = await _tenantRepo.GetByIdAsync(id);

            if (tenant == null)
            {
                return;
            }

            tenant.FirstName = tenantDto.FirstName;
            tenant.LastName = tenantDto.LastName;
            tenant.Email = tenantDto.Email;
            tenant.PhoneNumber = tenantDto.PhoneNumber;

            _tenantRepo.Update(tenant);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTenantAsync(int id)
        {
            var tenant = await _tenantRepo.GetByIdAsync(id);

            if (tenant == null)
            {
                return;
            }

            _tenantRepo.Delete(tenant);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    }
