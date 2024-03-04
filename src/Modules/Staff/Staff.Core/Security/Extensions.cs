using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Staff.Core.Domain.Entities;

namespace Staff.Core.Security;

internal static class Extensions
{
    internal static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPasswordHasher<Employee>, PasswordHasher<Employee>>()
            .AddSingleton<IPasswordManager, PasswordManager>();
    }
}
