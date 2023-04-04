using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.DAL.Entities;

namespace Property_Management.BLL.Interfaces
{
    public interface IManagerServices
    {
        Task<Response> AddProperty(AddOrUpdatePropertyRequest request);
        Task<Response> DeleteProperty(string propertyId);
        Task<Response> UpdateProperty(string propertyId, AddOrUpdatePropertyRequest request);
        Task<IEnumerable<Property>> GetAllProperties();
        Task<IEnumerable<Property>> GetAllAvaliableOrUnavialbleProperties(bool isAvailable);
        Task<IEnumerable<Property>> GetAllRentedOrNonRentedPropertiesByLandord(string landlordId, bool condiction);
    }
}
