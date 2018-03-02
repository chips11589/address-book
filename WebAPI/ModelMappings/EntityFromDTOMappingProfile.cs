using AutoMapper;
using DataAccess.Models;
using WebAPI.Services;

namespace WebAPI.ModelMappings
{
    public class EntityFromDTOMappingProfile : Profile
    {
        public EntityFromDTOMappingProfile()
        {
            CreateMap<ContactDTO, Contact>();
            CreateMap<TagDTO, Tag>();
        }
    }
}
