namespace Staff.Core.Security;

internal interface IPasswordManager
{
    string HashPassword(string password);
}
