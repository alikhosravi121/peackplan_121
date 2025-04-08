using System.ComponentModel.DataAnnotations;

namespace peackplan.Entities;

public class NoteEntity
{
    [Key]
    public Guid Id { get; set; } 
    public required string Content { get; set; }

    public required Guid SenderId { get; set; }

    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual List<NoteReceiverEntity> Receivers { get; set; } = new List<NoteReceiverEntity>();

}