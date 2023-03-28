using AutoMapper;
using Property_Management.BLL.DTOs.Request;
using Property_Management.DAL.Entities;

namespace Property_Management.API.ProfileMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ApplicationUser, UserRegistrationRequest>();
        }
    }
}
