using AutoMapper;
using Property_Management.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;
using Property_Management.DAL.Implementations;
using Property_Management.BLL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class TenantService : IPMSService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Tenant> _tenantRepo;

        public TenantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
        }
        public void Create(TenantVM model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tenant> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TenantWithTaskVM>> GetUsersWithTasksAsync()
        {

            return (await _tenantRepo.GetAllAsync(include: u => u.Include(t => t.TodoList))).Select(u => new TenantWithTaskVM
            {
                
                Tasks = u.TodoList.Select(t => new Property
                {
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate.ToString("d"),
                    Priority = t.Priority.ToString(),
                    Status = t.IsDone ? "Done" : "Not Done"
                })
            });
        }

    }