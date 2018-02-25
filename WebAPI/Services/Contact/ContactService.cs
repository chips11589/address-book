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
        private readonly IContactTagRepository _tagRepository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository,
            IContactTagRepository tagRepository,
            IMapper mapper)
        {
            _contactRepository = contactRepository;
            _tagRepository = tagRepository;
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
            var contacts = _contactRepository.GetAutoComplete(searchQuery);
            var contactDtos = contacts.Select(r => _mapper.Map<ContactAutoCompleteDTO>(r));

            return contactDtos;
        }

        public async Task<IEnumerable<ContactDTO>> GetContacts(string searchQuery)
        {
            var contacts = _contactRepository.Get(searchQuery);
            var contactDtos = contacts.Select(r => _mapper.Map<ContactDTO>(r));

            if (contactDtos.Any())
            {
                var tags = _tagRepository.GetByContactIds(contacts.Select(r => r.Id)).ToList();
                foreach (var contact in contactDtos)
                {
                    var tagsOfContact = tags.Where(r => r.Item1 == contact.Id)
                        .Select(r => _mapper.Map<TagDTO>(r.Item2));
                    contact.Tags.AddRange(tagsOfContact);
                }
            }

            return contactDtos;
        }
    }
}
