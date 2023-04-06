using Property_Management.BLL.DTOs.Request;
using Property_Management.DAL.Entities;

namespace Property_Management.BLL.Services
{
    public class UserType
    {
        public static LandLord NewLandLord(UserRegistrationRequest regRequest, string userId)
        {
            return new LandLord
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                FirstName = regRequest.Firstname,
                LastName = regRequest.LastName,
                Email = regRequest.Email,
                PhoneNumber = regRequest.MobileNumber,
                Occupation = regRequest.Occupation,
                Address = regRequest.Address,
        };
        }

        public static Tenant NewTenant(UserRegistrationRequest regRequest, string tenantId, string userId)
        {
            return new Tenant
            {
                TenantId = tenantId,
                UserId = userId,
                FirstName = regRequest.Firstname,
                LastName = regRequest.LastName,
                Email = regRequest.Email,
                PhoneNumber = regRequest.MobileNumber,
                Occupation = regRequest.Occupation,
                Address = regRequest.Address,
            };
        }

        public static Staff NewStaff(UserRegistrationRequest regRequest, string tenantId, string userId)
        {
            return new Staff
            {
                StaffId = tenantId,
                UserId = userId,
                FirstName = regRequest.Firstname,
                LastName = regRequest.LastName,
                Email = regRequest.Email,
                PhoneNumber = regRequest.MobileNumber,
                Occupation = regRequest.Occupation,
                Address = regRequest.Address,
            };
        }
    }
}
