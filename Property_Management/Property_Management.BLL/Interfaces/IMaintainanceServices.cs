using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;

namespace Property_Management.BLL.Interfaces
{
    public interface IMaintenanceRequestServices
    {
        Task<Response> CreateMaintenanceRequestAsync(MaintenanceRequestrequests requests);
        /*Task<(bool successful, string msg)> DeleteAsync(string TenantId, string MaintenanceId);
        (bool Done, string msg) ToggleMaintenaceStatus(string TenantId, string MaintenanceId);
        (MaintenanceRequest to, string msg) GetMaintenanceRequest(string TenantId, string MaintenanceId);
        IEnumerable<MaintenanceRequest> GetMaintenaceRequests();*/
    }
}