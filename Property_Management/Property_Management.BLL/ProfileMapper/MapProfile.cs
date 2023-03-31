﻿using AutoMapper;
using Property_Management.BLL.DTOs.Request;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;

namespace Property_Management.API.ProfileMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<UserRegistrationRequest, ApplicationUser>();
            CreateMap<AddPropertyRequest, Property>();
            CreateMap<AddPropertyRequest, LandLord>();
            CreateMap<CreateLeaseRequest, Lease>();
            CreateMap<AddOrUpdateMaintenanceVM, MaintenanceRequest>();
        }
    }
}
