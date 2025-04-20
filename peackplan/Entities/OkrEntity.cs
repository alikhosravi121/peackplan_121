using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using peackplan.Enums;

namespace peackplan.Entities;
[Table("Okrs")]
public class OkrEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public AccessLevelEnum AccessLevel { get; set; }
    [Required]
    public OkrStatusEnum Status { get; set; }

    public float PriorityWeight { get; set; } = 1.0f;

    public int? StartValue { get; set; }

    public int? CurrentValue { get; set; }

    public int? GoalValue { get; set; }

    public DateTime? DueDate { get; set; }

    public Guid? ParentOkrId { get; set; }
    public virtual OkrEntity? ParentOkr { get; set; }
    public Guid? AvatarId { get; set; }
    [Required]
    public Guid ManagerId { get; set; }
    public IEnumerable<UserEntity>? Users { get; set; }
    public IEnumerable<UploadedFileEntity>? UploadedFile { get; set; }
    public IEnumerable<NoteEntity>? NoteEntities { get; set; }
    public IEnumerable<PrimaryTaskEntity>? PrimaryTasks { get; set; }
}