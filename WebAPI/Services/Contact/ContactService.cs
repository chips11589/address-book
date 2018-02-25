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

        public Task<ContactDTO> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContactAutoCompleteDTO>> GetContactAutoComplete(string searchQuery)
        {
            var contacts = _contactRepository.Get(searchQuery);
            var contactDtos = contacts.Select(r => _mapper.Map<ContactAutoCompleteDTO>(r));

            return contactDtos;
        }

        public Task<IEnumerable<ContactDTO>> GetContacts()
        {
            throw new NotImplementedException();
        }
    }
}
