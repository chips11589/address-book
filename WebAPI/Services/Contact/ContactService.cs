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
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
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

            return contactDtos;
        }
    }
}
