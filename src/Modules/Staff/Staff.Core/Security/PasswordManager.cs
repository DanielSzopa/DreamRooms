using Microsoft.AspNetCore.Identity;
using Staff.Core.Domain.Entities;

namespace Staff.Core.Security;

public class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<Employee> _passwordHasher;

    public PasswordManager(IPasswordHasher<Employee> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string HashPassword(string password) => _passwordHasher.HashPassword(default, password);
}
