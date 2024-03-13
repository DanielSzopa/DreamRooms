using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Persistence;
public static class Extensions
{
    public static IServiceCollection RegisterDbContextType<T>(this IServiceCollection services)
        where T : DbContext
    {
        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<DbContextTypeRegistery>()
            .Register<T>();

        return services;
    }
}
