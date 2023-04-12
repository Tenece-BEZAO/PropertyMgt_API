using Property_Management.BLL.DTOs.Request;

namespace Property_Management.BLL.Interfaces;

public interface IRoleService
{
    Task AddUserToRole(AddUserToRoleRequest request);
    Task CreateRole(RoleDto request);
    Task DeleteRole(RoleDto request);
    Task EditRole(string id, RoleDto request);
    Task<IEnumerable<string>> GetUserRoles(string userName);
}