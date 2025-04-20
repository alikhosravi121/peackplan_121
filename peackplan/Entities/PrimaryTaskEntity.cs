using System.ComponentModel.DataAnnotations;
using peackplan.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace peackplan.Entities;

[Table("PrimaryTasks")]
public class PrimaryTaskEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = default!;

    public string? Description { get; set; }
    [Required]
    public OkrStatusEnum Status { get; set; }
    [Required]
    public AccessLevelEnum AccessLevel { get; set; }  // enum تعریف‌شده برای دسترسی‌ها

    public Guid? ParentTaskId { get; set; }
    
    public Guid? AvatarId { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Tags { get; set; } // کاما جدا یا json string

    [Required]
    public Guid ManagerId { get; set; } 
    
    public UserEntity? Manager { get; set; }
}