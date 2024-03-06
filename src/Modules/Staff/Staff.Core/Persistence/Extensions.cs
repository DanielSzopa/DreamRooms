using BuildingBlocks.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Staff.Core.Domain.Repositories;
using Staff.Core.Persistence.Repositories;

namespace Staff.Core.Persistence;

internal static class Extensions
{
    internal static IServiceCollection AddStaffPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddStaffDbContext(configuration)
            .AddUnitOfWork<StaffUnitOfWork>()
            .AddRepositories();
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

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IEmployeeRepository, EmployeeRepository>();
    }
}
