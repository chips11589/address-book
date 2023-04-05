using Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebAPI;

namespace Application.IntegrationTests
{
    public class TestBase
    {
        protected HttpClient _httpClient;
        protected Mock<IContactRepository> _contactRepository = new();

        [SetUp]
        public virtual void Init()
        {
            var hostBuilder = new WebHostBuilder()
                .ConfigureAppConfiguration((ctx, builder) =>
                {
                    builder.AddJsonFile("appsettings.json");
                })
                .ConfigureTestServices(OverrideServices)
                .UseStartup<Startup>();

            var testServer = new TestServer(hostBuilder);

            _httpClient = testServer.CreateClient();
        }

        protected virtual void OverrideServices(IServiceCollection services)
        {
            services.AddScoped(s => _contactRepository.Object);
        }
    }
}
