using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IApplicationReadDbConnection _readDbConnection;

        public ContactRepository(IApplicationReadDbConnection readDbConnection)
        {
            _readDbConnection = readDbConnection;
        }

        public async Task<List<Contact>> GetContactsAsync(string searchQuery)
        {
            var contacts = new Dictionary<Guid, Contact>();
            await _readDbConnection
                .QueryAsync<Contact, Tag, Contact>(
                    "EXECUTE dbo.GetContacts @searchQuery",
                    (contact, tag) => LinkContactWithTags(contacts, contact, tag),
                    new { searchQuery }
                );

            return contacts.Values.ToList();
        }

        public async Task<List<Contact>> GetContactsAsync(int top)
        {
            var contacts = new Dictionary<Guid, Contact>();
            await _readDbConnection
                .QueryAsync<Contact, Tag, Contact>(
                    $"SELECT TOP {top} c.*, t.* " +
                    "FROM Contacts c " +
                    "LEFT JOIN ContactTag ct ON c.Id = ct.ContactsId " +
                    "LEFT JOIN Tags t ON t.Id = ct.TagsId",
                    (contact, tag) => LinkContactWithTags(contacts, contact, tag)
            );

            return contacts.Values.ToList();
        }

        private static Contact LinkContactWithTags(Dictionary<Guid, Contact> contacts, Contact contact, Tag tag)
        {
            if (!contacts.TryGetValue(contact.Id, out Contact contactEntity))
            {
                contacts.Add(contact.Id, contact);
                contactEntity = contact;
            }

            if (tag != null)
            {
                contactEntity.Tags.Add(tag);
            }
            return contactEntity;
        }
    }
}
