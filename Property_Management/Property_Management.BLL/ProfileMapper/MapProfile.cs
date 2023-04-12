using AutoMapper;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;

namespace Property_Management.API.ProfileMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<UserRegistrationRequest, ApplicationUser>();
            CreateMap<AddOrUpdatePropertyRequest, Property>();
            CreateMap<AddOrUpdatePropertyRequest, LandLord>();
            CreateMap<CreateLeaseRequest, Lease>();

            CreateMap<TenantDTO, Tenant>();

            CreateMap<PaymentRequest, Transaction>();

        }
    }
}
