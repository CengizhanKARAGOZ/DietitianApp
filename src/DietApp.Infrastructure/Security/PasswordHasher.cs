using DietApp.Application.Common.Interfaces;
using BC = BCrypt.Net.BCrypt;
namespace DietApp.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{

    public string Hash(string password)
    {
        return BC.HashPassword(password, BC.GenerateSalt(12));
    }

    public bool Verify(string password, string hash)
    {
        return BC.Verify(password, hash);
    }
}
