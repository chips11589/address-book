using Application.Contacts;
using Domain.Entities;
using FluentAssertions;
using Moq;
using System.Net.Http.Json;

namespace Application.IntegrationTests
{
    public class ContactControllerTests : TestBase
    {
        [Test]
        public async Task Get_GivenExistingContacts_ReturnContacts()
        {
            _contactRepository
                .Setup(mock => mock.GetContactsAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<Contact>
                {
                    new Contact { Title = "Mr", FirstName = "Chips" },
                    new Contact { Title = "Mrs", FirstName = "Helen" }
                });

            var response = await _httpClient.GetAsync("api/contact");
            var data = await response.Content.ReadFromJsonAsync<List<ContactDto>>();

            data.Count.Should().Be(2);
            data[0].FirstName.Should().Be("Chips");
            data[1].FirstName.Should().Be("Helen");
        }
    }
}