using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Staff.Core.Persistence;
internal class StaffDbContextInitializator : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<StaffDbContextInitializator> _logger;

    public StaffDbContextInitializator(IServiceProvider serviceProvider, ILogger<StaffDbContextInitializator> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<StaffDbContext>();

        _logger.LogInformation("Check {0} migrations......", nameof(StaffDbContext));

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
        if (pendingMigrations is null || !pendingMigrations.Any())
        {
            _logger.LogInformation("Migrations from {0} are up to date", nameof(StaffDbContext));
            return;
        }

        await dbContext.Database.MigrateAsync(cancellationToken);
        _logger.LogInformation("Migrations from {0} are applied", nameof(StaffDbContext));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
