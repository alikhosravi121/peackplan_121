using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace peackplan.Entities;

[Table("UploadedFiles")]
public class UploadedFileEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(300)]
    public string FileName { get; set; } = default!;

    [Required]
    [MaxLength(500)]
    public string FilePath { get; set; } = default!;

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    
    public UserEntity? Users { get; set; } 
}