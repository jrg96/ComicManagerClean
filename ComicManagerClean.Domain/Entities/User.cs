using System.ComponentModel.DataAnnotations;

namespace ComicManagerClean.Domain.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [StringLength(50)]
    public string Email { get; set; }

    public string Password { get; set; }

    [MaxLength(128)]
    public byte[] Salt { get; set; }
}
