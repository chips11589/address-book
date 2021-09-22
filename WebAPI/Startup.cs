using DataAccess;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI.Hubs;
using WebAPI.Services.Contact;
using WebAPI.Services.Notification;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Need to specify origins otherwise Cors policies for Signal Core won't work
            // https://github.com/aspnet/SignalR/issues/2110
            var allowedOrigins = Configuration
                .GetSection("Cors")["AllowedOrigins"]?.Split(",");

            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Add Db context
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("DataAccess"))
            );
            services.AddScoped<IContactRepository>(provider => new ContactRepository(provider.GetService<ApplicationDbContext>(), connectionString));
            services.AddScoped<IContactTagRepository, ContactTagRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactTagService, ContactTagService>();    
            services.AddMvc();

            // Register SignalR
            services.AddSignalR();
            services.AddSingleton<INotificationService, NotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            });
        }
    }
}
