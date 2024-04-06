using BuildingBlocks.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Core.Domain.Repositories;
using Reservations.Core.Persistence.Repositories;

namespace Reservations.Core.Persistence;
internal static class Extensions
{
    internal static IServiceCollection AddReservationsPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IReceptionistsRepository, ReceptionistsRepository>()
            .AddUnitOfWork<ReservationsUnitOfWork>()
            .AddReservationsDbContext(configuration);
    }

    private static IServiceCollection AddReservationsDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<ReservationsDbContext>(options =>
            {
                var connectionString = configuration.GetValue<string>("ConnectionStrings:Default");
                options.UseSqlServer(connectionString, x => x.MigrationsHistoryTable("EfMigrations", Constants.ReservationsSchema));
            });
    }
}
