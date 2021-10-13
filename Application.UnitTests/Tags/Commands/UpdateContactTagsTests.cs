using Application.Common.Interfaces;
using Application.Tags;
using Application.Tags.Commands.UpdateContactTags;
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
        [Test]
        public async Task UpdateContactTagsCommand_ShouldUpdateContactTagsAsRequestedAsync()
        {
            var services = new ServiceCollection();
            services.AddApplication();

            var newTags = new List<TagDto>
            {
                new TagDto { Id = Guid.NewGuid(), Name = "Tag 1" },
                new TagDto { Id = Guid.NewGuid(), Name = "Tag 2" },
                new TagDto { Id = Guid.NewGuid(), Name = "Tag 3" }
            };

            var contacts = new List<Contact>
            {
                new Contact { Id = Guid.NewGuid(), FirstName = "Chips 1", Surname = "Test" },
                new Contact { Id = Guid.NewGuid(), FirstName = "Chips 2", Surname = "Test" }
            };
            var mockContactDbSet = contacts.AsQueryable().BuildMockDbSet();
            var mockDbContext = new Mock<IApplicationDbContext>();
            mockDbContext.SetupGet(mock => mock.Contacts).Returns(() =>
            {
                return mockContactDbSet.Object;
            });
            var dbContextObject = mockDbContext.Object;

            services.AddScoped(provider => dbContextObject);

            var serviceProvider = services.BuildServiceProvider();
            var mediator = serviceProvider.GetService<ISender>();
            var contactEntity = await dbContextObject.Contacts
                .FirstOrDefaultAsync(contact => contact.Id == contacts[0].Id);

            await mediator.Send(new UpdateContactTagsCommand
            {
                ContactId = contacts[0].Id,
                Tags = newTags
            });
            Assert.AreEqual(3, contactEntity.Tags.Count);
            Assert.AreEqual(newTags[0].Id, contactEntity.Tags[0].Id);


            await mediator.Send(new UpdateContactTagsCommand
            {
                ContactId = contacts[0].Id,
                Tags = new List<TagDto>
                {
                    newTags[1],
                    newTags[2]
                }
            });
            Assert.AreEqual(2, contactEntity.Tags.Count);
            Assert.AreEqual(newTags[1].Id, contactEntity.Tags[0].Id);
            Assert.AreEqual(newTags[2].Id, contactEntity.Tags[1].Id);


            await mediator.Send(new UpdateContactTagsCommand
            {
                ContactId = contacts[0].Id,
                Tags = new List<TagDto>()
            });
            Assert.AreEqual(0, contactEntity.Tags.Count);
        }
    }
}
