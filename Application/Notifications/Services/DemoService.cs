using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Notifications.Services
{
    public class DemoService : BackgroundService
    {
        private readonly ILogger<DemoService> _logger;
        private readonly INotificationService _notificationService;

        public DemoService(ILogger<DemoService> logger, INotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("DemoService running.");

            using (PeriodicTimer timer = new(TimeSpan.FromSeconds(20)))
            {
                try
                {
                    while (await timer.WaitForNextTickAsync(stoppingToken))
                    {
                        await _notificationService.NotifyAsync(new NotificationDto
                        {
                            Message = "Hello! Notifications appear every time you change a tag"
                        });
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("DemoService is stopping.");
                }
                catch (Exception ex)
                {
                    // TODO: Change this to scoped service so it can be respawned
                    _logger.LogError(ex, "DemoService has crashed.");
                }
            }
        }
    }
}
