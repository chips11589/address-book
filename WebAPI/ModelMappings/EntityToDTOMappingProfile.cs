﻿using AutoMapper;
using DataAccess.Models;
using WebAPI.Services;

namespace WebAPI.ModelMappings
{
    public class EntityToDTOMappingProfile : Profile
    {
        public EntityToDTOMappingProfile()
        {
            CreateMap<Contact, ContactDTO>();
            CreateMap<Contact, ContactAutoCompleteDTO>();
        }
    }
}
