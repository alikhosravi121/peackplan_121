using System.ComponentModel.DataAnnotations;

namespace peackplan.Entities;

public class TagEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = default!;
    
    public required CompanyEntity? Company { get; set; }
}