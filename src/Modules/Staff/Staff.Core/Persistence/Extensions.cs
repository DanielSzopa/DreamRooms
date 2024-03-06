using BuildingBlocks.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Staff.Core.Persistence;

internal static class Extensions
{
    internal static IServiceCollection AddStaffPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddStaffDbContext(configuration)
            .AddUnitOfWork<StaffUnitOfWork>();
    }

    private static IServiceCollection AddStaffDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<StaffDbContext>(options =>
            {
                var connectionString = configuration.GetValue<string>("ConnectionStrings:Default");
                options.UseSqlServer(connectionString);
            });
    }
}
