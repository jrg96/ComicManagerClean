using ComicManagerClean.Application.Services.Security;
using System.Security.Cryptography;
using System.Text;
namespace ComicManagerClean.Infrastructure.Services.Security;

public class PasswordSecurityService : IPasswordSecurityService
{
    public (string hashedPassword, byte[] salt) EncryptPassword(string password)
    {
        const int keySize = 64;
        byte[] salt = RandomNumberGenerator.GetBytes(keySize);

        return (EncryptPassword(password, salt), salt);
    }

    public string EncryptPassword(string password, byte[] salt)
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);

        return Convert.ToHexString(hash);
    }
}
