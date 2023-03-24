using Application.Common.Interfaces;
using Application.Tags;
using Application.Tags.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UnitTests.Tags.Commands
{
    public class UpdateContactTagsCommandTests
    {
        private IApplicationDbContext _dbContextObject;
        private List<TagDto> _tags;
        private List<Contact> _contacts;
        private ISender _mediator;
        private Contact _contactEntity;

        [SetUp]
        public async Task Init()
        {
            var services = new ServiceCollection();
            services.AddApplication();

            _tags = new List<TagDto>
            {
                new TagDto { Id = Guid.NewGuid(), Name = "Tag 1" },
                new TagDto { Id = Guid.NewGuid(), Name = "Tag 2" },
                new TagDto { Id = Guid.NewGuid(), Name = "Tag 3" }
            };

            _contacts = new List<Contact>
            {
                new Contact { Id = Guid.NewGuid(), FirstName = "Chips 1", Surname = "Test" },
                new Contact { Id = Guid.NewGuid(), FirstName = "Chips 2", Surname = "Test" }
            };
            var mockContactDbSet = _contacts.AsQueryable().BuildMockDbSet();
            var mockDbContext = new Mock<IApplicationDbContext>();
            mockDbContext.SetupGet(mock => mock.Contacts).Returns(() =>
            {
                return mockContactDbSet.Object;
            });
            _dbContextObject = mockDbContext.Object;

            services.AddScoped(provider => _dbContextObject);

            var serviceProvider = services.BuildServiceProvider();
            _mediator = serviceProvider.GetService<ISender>();

            _contactEntity = await _dbContextObject.Contacts
                .FirstOrDefaultAsync(contact => contact.Id == _contacts[0].Id);
        }

        [Test]
        public async Task AddTags_ShouldUpdateContactTagsAsRequestedAsync()
        {
            await AddTagsAsync();

            Assert.AreEqual(3, _contactEntity.Tags.Count);
            Assert.AreEqual(_tags[0].Id, _contactEntity.Tags[0].Id);
        }

        [Test]
        public async Task UpdateTags_ShouldUpdateContactTagsAsRequestedAsync()
        {
            await AddTagsAsync();

            await _mediator.Send(new UpdateContactTagsCommand
            {
                ContactId = _contacts[0].Id,
                Tags = new List<TagDto>
                {
                    _tags[1],
                    _tags[2]
                }
            });

            Assert.AreEqual(2, _contactEntity.Tags.Count);
            Assert.AreEqual(_tags[1].Id, _contactEntity.Tags[0].Id);
            Assert.AreEqual(_tags[2].Id, _contactEntity.Tags[1].Id);

            Assert.Fail();
        }

        [Test]
        public async Task RemoveTags_ShouldUpdateContactTagsAsRequestedAsync()
        {
            await AddTagsAsync();

            await _mediator.Send(new UpdateContactTagsCommand
            {
                ContactId = _contacts[0].Id,
                Tags = new List<TagDto>()
            });

            Assert.AreEqual(0, _contactEntity.Tags.Count);
        }

        private async Task AddTagsAsync()
        {
            await _mediator.Send(new UpdateContactTagsCommand
            {
                ContactId = _contacts[0].Id,
                Tags = _tags
            });
        }
    }
}
