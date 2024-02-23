namespace ComicManagerClean.Application.Services.Security;

public interface IPasswordSecurityService
{
    (string hashedPassword, byte[] salt) EncryptPassword(string password);
    bool VerifyPassword(string password, string hash, byte[] salt);
}
