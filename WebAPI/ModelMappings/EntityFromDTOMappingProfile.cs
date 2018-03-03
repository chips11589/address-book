using AutoMapper;
using DataAccess.Models;
using WebAPI.Services.Contact;

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
