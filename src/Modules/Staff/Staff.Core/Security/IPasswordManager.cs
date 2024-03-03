namespace Staff.Core.Security;

public interface IPasswordManager
{
    string HashPassword(string password);
}
