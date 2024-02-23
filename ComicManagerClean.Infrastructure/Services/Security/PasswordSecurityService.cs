using ComicManagerClean.Application.Services.Security;
using System.Security.Cryptography;
using System.Text;
namespace ComicManagerClean.Infrastructure.Services.Security;

public class PasswordSecurityService : IPasswordSecurityService
{
    private readonly int keySize = 32;
    private readonly int iterations = 350000;
    private readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public (string hashedPassword, byte[] salt) EncryptPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);

        return (Convert.ToHexString(hash), salt);
    }

    public bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}
