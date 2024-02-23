namespace ComicManagerClean.Application.Services.Security;

public interface IPasswordSecurityService
{
    (string hashedPassword, byte[] salt) EncryptPassword(string password);
}
