using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;

namespace Application.Tags
{
    public class TagDto : IMapFrom<Tag>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(Tag), GetType()).ReverseMap();
        }
    }
}
