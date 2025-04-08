using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace peackplan.Entities;

[Table("Users")]
public class UserEntity
{
    [Key]
    public Guid Id { get; set; }
    [MinLength(4)]
    [MaxLength(100)]
    public required string Fullname { get; set; }
    [MinLength(10)]
    [MaxLength(12)]
    public required string  PhoneNumber { get; set; }
    [EmailAddress]
    public string  Email { get; set; }
    public required string Password { get; set; }
     
   
    public DateTime? Birthday { get; set; }
    public bool IsMarried { get; set; } = false;
    public string? FileSrc {get;set;}
    public List<TeamWorkEntity>? TeamWorks { get; set; }
    
    public Guid? AvatarId { get; set; }
    public IEnumerable<UploadedFileEntity>? UploadedFile { get; set; }
    

}