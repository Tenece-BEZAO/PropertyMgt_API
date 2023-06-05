using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;

namespace Property_Management.BLL.Interfaces
{
    public interface IMaintenanceServices
    {
        Task<bool> AddMaintenanceAsync(AddMaintenanceRequest request);
        Task<bool> DeleteRequestAsync(string mrId);
        Task<bool> UpdateRequestAsync(UpdateMaintenanceRequest request);
        Task<MrResponse> GetRequestAsync(string mrId);
        Task<IEnumerable<MrResponse>> GetAllRequestAsync();
    }
}
