using Application;
using Application.Common.Models;
using Domain.Events;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebAPI.EventHandlers;
using WebAPI.Filters;
using WebAPI.Hubs;
using WebAPI.Queries;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddGraphQLServer()
                .AddQueryType()
                .AddTypeExtension<ContactQuery>()
                .AddTypeExtension<TagQuery>()
                .AddType<ContactType>();

            // Need to specify origins otherwise Cors policies for Signal Core won't work
            // https://github.com/aspnet/SignalR/issues/2110
            var allowedOrigins = Configuration
                .GetSection("Cors")["AllowedOrigins"]?.Split(",");

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc(options => options.Filters.Add<ApiExceptionFilterAttribute>());

            // Register SignalR
            services.AddSignalR();
            services.AddTransient<INotificationHandler<DomainEventNotification<TagChangedEvent>>, TagChangedEventHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine($"Configure Environment: {env.EnvironmentName}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("/notificationHub");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGraphQL();
            });
        }
    }
}
