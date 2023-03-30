using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;

namespace Property_Management.BLL.Interfaces
{
    public interface ILandLordServices
    {
        Task<Response> AddProperty(AddPropertyRequest request);
    }
}
