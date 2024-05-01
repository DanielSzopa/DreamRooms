using BuildingBlocks.Persistence;
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
            .RegisterDbContextType<StaffDbContext>()
            .AddUnitOfWork<StaffUnitOfWork>()
            .AddRepositories()
            .AddHostedService<StaffDbContextInitializator>();
    }

    private static IServiceCollection AddStaffDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<StaffDbContext>(options =>
            {
                var connectionString = configuration.GetValue<string>("ConnectionStrings:Default");
                options.UseSqlServer(connectionString, x => x.MigrationsHistoryTable("EfMigrations", Constants.StaffSchema));
            });
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IEmployeeRepository, EmployeeRepository>();
    }
}
