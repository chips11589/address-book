using AutoMapper;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IContactTagRepository _contactTagRepository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository,
            IContactTagRepository contactTagRepository,
            IMapper mapper)
        {
            _contactRepository = contactRepository;
            _contactTagRepository = contactTagRepository;
            _mapper = mapper;
        }

        public async Task<ContactDTO> Get(Guid id)
        {
            var contact = await _contactRepository.GetByID(id);
            var contactDto = _mapper.Map<ContactDTO>(contact);

            return contactDto;
        }

        public async Task<IEnumerable<ContactAutoCompleteDTO>> GetContactAutoComplete(string searchQuery)
        {
            var contacts = _contactRepository.GetAutoComplete(searchQuery)
                .OrderBy(r => r.Suggestion)
                .ToList();
            var contactDtos = contacts.Select(r => _mapper.Map<ContactAutoCompleteDTO>(r));

            return contactDtos;
        }

        public async Task<IEnumerable<ContactDTO>> GetContacts(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return new List<ContactDTO>();
            }

            var contacts = _contactRepository.Get(searchQuery).ToList();
            var contactDtos = contacts.Select(r => _mapper.Map<ContactDTO>(r)).ToList();

            if (contactDtos.Any())
            {
                AddTagsToContact(contactDtos);
            }

            return contactDtos;
        }

        public async Task<IEnumerable<ContactDTO>> GetContactsByTag(Guid tagId)
        {
            var contactTags = _contactTagRepository.Get(r => r.TagId == tagId);
            var contacts = contactTags.Select(r => r.Contact).ToList();
            var contactDtos = contacts.Select(r => _mapper.Map<ContactDTO>(r)).ToList();

            if (contactDtos.Any())
            {
                AddTagsToContact(contactDtos);
            }

            return contactDtos;
        }

        private void AddTagsToContact(List<ContactDTO> contactDtos)
        {
            var tags = _contactTagRepository.GetByContactIds(contactDtos.Select(r => r.Id)).ToList();
            foreach (var contact in contactDtos)
            {
                var tagsOfContact = tags.Where(r => r.Item1 == contact.Id)
                    .Select(r => _mapper.Map<TagDTO>(r.Item2));
                contact.Tags.AddRange(tagsOfContact);
            }
        }
    }
}
