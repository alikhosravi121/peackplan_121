using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace peackplan.Entities;

[Table("Companies")]
public class CompanyEntity
{
    [Key]
    public required Guid  Id { get; set; }
    public required string Title { get; set; }
    
    public IEnumerable<TeamWorkEntity>? TeamWorks { get; set; }
    
}