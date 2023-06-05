using AutoMapper;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
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
            CreateMap<Tenant, TenantResponse>();
            CreateMap<PaymentRequest, Transaction>();
            CreateMap<MaintenanceRequest, AddMaintenanceRequest>().ReverseMap();
            CreateMap<Review, SaveReviewRequest>().ReverseMap();
            CreateMap<NewUnitRequest, Unit>()
                .ForMember(src => src.Description, des => des.MapFrom(des => des.UnitDescription))
                .ForMember(src => src.Name, des => des.MapFrom(des => des.UnitName));
            CreateMap<EmailRequests, Email>()
                .ForMember(des => des.ReceiverEmail, src => src.MapFrom(src => src.To))
                .ForMember(des => des.SenderEmail, src => src.MapFrom(src => src.From));

        }
    }
}
