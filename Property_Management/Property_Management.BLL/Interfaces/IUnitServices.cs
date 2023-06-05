using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.DAL.Entities;

namespace Property_Management.BLL.Interfaces
{
    public interface IUnitServices
    {
        Task<bool> CreateUnitAsync(NewUnitRequest request);
        Task<UnitResponse> UpdateUnitAsync(NewUnitRequest request);
        Task<bool> DeleteUnitAsync(string unitId);
        Task<UnitResponse> GetUnitAsync(string unitId);
        Task<IEnumerable<UnitResponse>> GetUnitsAsync(RequestParameters requestParam);
    }
}
