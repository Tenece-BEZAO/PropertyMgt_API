

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;

using Microsoft.AspNetCore.Authentication;
using Property_Management.DAL.Interfaces;
using Property_Management.BLL.DTOs.Request;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Mvc;
using Property_Management.DAL.Context;

namespace Property_Management.BLL.Implementations
{
    public class TenantServices : ITenantServices
    {

        private readonly IRepository<Tenant> _tenantRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAuth _authService;
        private readonly PMSDbContext _context;
        public TenantServices(IUnitOfWork unitOfWork, IMapper mapper, IUserAuth authService, PMSDbContext context)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<string> CreateTenant(TenantDTO tenant)
        {
            if (tenant.UserId == null || tenant.UserId == "")
            {
                var CreatedUserIdInfo = await _authService.CreateUserAsync(new UserRegistrationRequest { Email = tenant.Email });


            }

            Tenant newTenant = new Tenant
            {
                TenantId = Guid.NewGuid().ToString(),
                UserId = tenant.UserId,
                FirstName = tenant.FirstName,
                LastName = tenant.LastName,
                Address = tenant.Address,
                Email = tenant.Email,
                Occupation = tenant.Occupation,
                PhoneNumber = tenant.PhoneNumber,
                MoveInDate = tenant.MoveInDate,
                MoveOutDate = tenant.MoveOutDate,
            };


            var info = await _tenantRepo.AddAsync(newTenant);
            if (info != null)
                return "Tenant with id:" + info.TenantId + " has been created";

            throw new NotImplementedException("Tenant could not be created");

        }


        public async Task<bool> DeleteTenant(string id)
        {
            var result = _tenantRepo.DeleteByIdAsync(id);
            if (result.IsCompletedSuccessfully) return true; return false;

        }

        public async Task<TenantDTO> EditTenant(string id, TenantDTO tenantDto)
        {
            var tenant = await _tenantRepo.GetByIdAsync(id);

            if (tenant == null)
            {
                return null;
            }

            /*tenant.UserId = tenantDto.UserId;
            tenant.Email = tenantDto.Email;
            tenant.PhoneNumber = tenantDto.PhoneNumber;
            tenant.Address = tenantDto.Address;
            tenant.Occupation = tenantDto.Occupat*/
            // update other properties



            return TenantDTO.FromTenant(tenant);
        }
        /*public Task<bool> EditTenant(TenantDTO tenant)
        {
            Tenant UpdateTenant = new Tenant
            {
                UserId = tenant.UserId,
                FirstName = tenant.FirstName,
                LastName = tenant.LastName,
                Address = tenant.Address,
                Email = tenant.Email,
                Occupation = tenant.Occupation,
                PhoneNumber = tenant.PhoneNumber,
                MoveInDate = tenant.MoveInDate,
                MoveOutDate = tenant.MoveOutDate,
            };
            var result = _tenantRepo.UpdateAsync(UpdateTenant);

            if (result.IsCompletedSuccessfully) return true; return false;



        }*/



       /* public async Task<IEnumerable<TenantDTO>> GetAllTenants()
        {
            var tenants = await _tenantRepo.GetAllAsync();

            return tenants.Select(tenant => new TenantDTO
            {
                UserId = tenant.TenantId,
                FirstName = tenant.FirstName,
                LastName = tenant.LastName,
                Email = tenant.Email,
                PhoneNumber = tenant.PhoneNumber,
                MoveInDate = tenant.MoveInDate,
                MoveOutDate = tenant.MoveOutDate,

            });
        }*/
        /* public async Task<TenantDTO> GetTenantById(string id)
         {
             var tenant = await  _tenantRepo.GetByIdAsync(id);
             if (tenant == null)
                 throw new NotFoundException("No tenant was found");

             return TenantDTO.FromTenant(tenant);

         }*/
        public async Task<IEnumerable<TenantDTO>> GetAllTenants()
        {
            var tenants = await _context.Tenants.ToListAsync();
            var tenantDtos = new List<TenantDTO>();

            foreach (var tenant in tenants)
            {
                var leases = await _context.Leases
                    .Include(l => l.Unit)
                    .Where(l => l.TenantId == tenant.TenantId)
                    .Select(l => new LeaseDto
                    {
                        LeaseId = l.Id,
                        StartDate = l.StartDate,
                        EndDate = l.EndDate,
                        Unit = new UnitDto
                        {
                            UnitId = l.Unit.UnitId,
                            Name = l.Unit.Name
                        }
                    })
                    .ToListAsync();

                var tenantDto = new TenantDTO
                {
                    TenantId = tenant.TenantId,
                    FirstName = tenant.FirstName,
                    LastName = tenant.LastName,
                    Address = tenant.Address,
                    Email = tenant.Email,
                    Occupation = tenant.Occupation,
                    PhoneNumber = tenant.PhoneNumber,
                    MoveInDate = tenant.MoveInDate,
                    MoveOutDate = tenant.MoveOutDate,
                    Leases = leases
                };
                tenantDtos.Add(tenantDto);
            }

            return tenantDtos;
        }

        public async Task<TenantDTO> GetTenantById(string id)
        {
            var tenant = await _context.Tenants.FindAsync(id);

            if (tenant == null)
            {
                return null;
            }

            var leases = await _context.Leases
                .Include(l => l.Unit)
                .Where(l => l.TenantId == id)
                .Select(l => new LeaseDto
                {
                    LeaseId = l.Id,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    Unit = new UnitDto
                    {
                        UnitId = l.Unit.UnitId,
                        Name = l.Unit.Name
                    }
                })
                .ToListAsync();

            var tenantDto = new TenantDTO
            {
                TenantId = tenant.TenantId,
                FirstName = tenant.FirstName,
                LastName = tenant.LastName,
                Address = tenant.Address,
                Email = tenant.Email,
                Occupation = tenant.Occupation,
                PhoneNumber = tenant.PhoneNumber,
                MoveInDate = tenant.MoveInDate,
                MoveOutDate = tenant.MoveOutDate,
                Leases = leases
            };
            return tenantDto;
        }




    }
}
