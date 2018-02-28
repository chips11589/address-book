using AutoMapper;
using DataAccess.Models;
using DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class ContactTagService : IContactTagService
    {
        private readonly IContactTagRepository _contactTagRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public ContactTagService(IContactTagRepository contactTagRepository,
            ITagRepository tagRepository,
            IMapper mapper)
        {
            _contactTagRepository = contactTagRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagDTO>> GetTags()
        {
            var tags = _tagRepository.Get().ToList();
            var tagDtos = tags.Select(r => _mapper.Map<TagDTO>(r));

            return tagDtos;
        }

        public async Task CreateTag(TagDTO tag)
        {
            var tagEntity = _mapper.Map<Tag>(tag);
            _tagRepository.Insert(tagEntity);

            await _tagRepository.DbContext.SaveChangesAsync();
        }

        public async Task UpdateTag(TagDTO tag)
        {
            var tagEntity = _mapper.Map<Tag>(tag);
            _tagRepository.Update(tagEntity);

            await _tagRepository.DbContext.SaveChangesAsync();
        }

        public async Task RemoveTag(TagDTO tag)
        {
            var tagEntity = _mapper.Map<Tag>(tag);
            _tagRepository.Delete(tagEntity);

            await _tagRepository.DbContext.SaveChangesAsync();
        }

        public async Task UpdateContactTags(ContactDTO contact)
        {
            await _contactTagRepository.DeleteByContactId(contact.Id);

            var contactTags = new List<ContactTag>();
            foreach(var tag in contact.Tags)
            {
                contactTags.Add(new ContactTag
                {
                    ContactId = contact.Id,
                    TagId = tag.Id
                });
            }
            await _contactTagRepository.InsertContactTags(contactTags);

            await _contactTagRepository.DbContext.SaveChangesAsync();
        }
    }
}
