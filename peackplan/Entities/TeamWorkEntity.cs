using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace peackplan.Entities;

[Table("TeamWorks")]
public class TeamWorkEntity
{
    [Key]
    public required Guid  Id { get; set; }

    public required string Title { get; set; }
    
    public Guid?  CompanyId { get; set; } 
    public CompanyEntity? Company { get; set; }
    
    public IEnumerable<UserEntity>? Users { get; set; }
}