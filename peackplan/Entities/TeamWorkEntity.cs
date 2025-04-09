using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace peackplan.Entities;

[Table("TeamWorks")]
public class TeamWorkEntity
{
    [Key]
    public required Guid  Id { get; set; }
    [Required]
    [MinLength(4)]
    [MaxLength(100)]
    public required string Title { get; set; }
    [MaxLength(200)]
    public  string? Target { get; set; }
    [Required]
    public  Guid AdminId { get; set; }
    
    public Guid? AvatarId { get; set; }
    public Guid?  CompanyId { get; set; } 
    public Guid?  OwnerTeamWrokId { get; set; } 
    [MaxLength(300)]
    public string? FileSrc {get;set;}
    public CompanyEntity? Company { get; set; } 
    public IEnumerable<UserEntity>? Users { get; set; } 
}