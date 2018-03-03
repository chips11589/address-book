using AutoMapper;
using DataAccess.Models;
using WebAPI.Services.Contact;

namespace WebAPI.ModelMappings
{
    public class EntityToDTOMappingProfile : Profile
    {
        public EntityToDTOMappingProfile()
        {
            CreateMap<Contact, ContactDTO>();
            CreateMap<ContactAutoComplete, ContactAutoCompleteDTO>();
            CreateMap<Tag, TagDTO>();
        }
    }
}
