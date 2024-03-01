using ComicManagerClean.Domain.Shared.Enums;

namespace ComicManagerClean.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public byte[] Salt { get; set; }
    public RolesEnum Role { get; set; }
}
