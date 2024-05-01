using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Reservations.Core.Persistence;
internal class ReservationsDbContextInitializator : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ReservationsDbContextInitializator> _logger;

    public ReservationsDbContextInitializator(IServiceProvider serviceProvider, ILogger<ReservationsDbContextInitializator> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ReservationsDbContext>();

        _logger.LogInformation("Check {0} migrations......", nameof(ReservationsDbContext));

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
        if(pendingMigrations is null || !pendingMigrations.Any())
        {
            _logger.LogInformation("Migrations from {0} are up to date", nameof(ReservationsDbContext));
            return;
        }

        await dbContext.Database.MigrateAsync(cancellationToken);
        _logger.LogInformation("Migrations from {0} are applied", nameof(ReservationsDbContext));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
