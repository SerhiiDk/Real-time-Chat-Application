using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace ChatApplication.API.V1.Extensions;

public static class PasswordHasher
{
    public static string GenerateHash(this string password)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashBytes = SHA256.HashData(passwordBytes);

        return Convert.ToBase64String(hashBytes);
    }
}
