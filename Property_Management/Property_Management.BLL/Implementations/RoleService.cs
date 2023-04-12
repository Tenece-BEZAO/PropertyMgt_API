using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations;

public class RoleService : IRoleService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IServiceFactory _serviceFactory;
    private readonly IMapper _mapper;
    private readonly IRepository<ApplicationRole> _roleRepo;

    private readonly IUnitOfWork _unitOfWork;


    public RoleService(IServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
        _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
        _userManager = _serviceFactory.GetService<UserManager<ApplicationUser>>();
        _roleManager = _serviceFactory.GetService<RoleManager<ApplicationRole>>();
        _roleRepo = _unitOfWork.GetRepository<ApplicationRole>();
        _mapper = _serviceFactory.GetService<IMapper>();
    }

    public async Task AddUserToRole(AddUserToRoleRequest request)
    {
        ApplicationUser user = await _userManager.FindByNameAsync(request.UserName.Trim().ToLower());

        if (user == null)
            throw new InvalidOperationException($"User '{request.UserName}' does not Exist!");

        await _userManager.AddToRoleAsync(user, request.Role.ToLower().Trim());


    }

    public async Task CreateRole(RoleDto request)
    {
        ApplicationRole role = await _roleManager.FindByNameAsync(request.Name.Trim().ToLower());

        if (role != null)
            throw new InvalidOperationException($"Role with name {request.Name} already exist");

        ApplicationRole roleToCreate = _mapper.Map<ApplicationRole>(request);

        await _roleManager.CreateAsync(roleToCreate);
    }

    public async Task DeleteRole(RoleDto request)
    {
        ApplicationRole role = await _roleManager.FindByNameAsync(request.Name.Trim().ToLower());

        if (role is null)
            throw new InvalidOperationException($"Role {request.Name} does not Exist");

        await _roleManager.DeleteAsync(role);

    }

    public async Task EditRole(string id, RoleDto request)
    {
        ApplicationRole role = await _roleManager.FindByIdAsync(id);
        if (role == null)
            throw new InvalidOperationException($"Role with {id} not found");

        ApplicationRole roleUpdate = _mapper.Map(request, role);

        await _roleManager.UpdateAsync(roleUpdate);
    }

    public async Task<IEnumerable<string>> GetUserRoles(string userName)
    {
        ApplicationUser user = await _userManager.FindByNameAsync(userName.Trim().ToLower());
        if (user == null)
            throw new InvalidOperationException($"User with username {userName} not found");

        List<string> userRoles = (List<string>)await _userManager.GetRolesAsync(user);
        if (!userRoles.Any())
            throw new InvalidOperationException($"User {userName} has no role");

        return userRoles;
    }

}