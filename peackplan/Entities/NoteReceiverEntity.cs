using System.ComponentModel.DataAnnotations;

namespace peackplan.Entities;

public class NoteReceiverEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid NoteId { get; set; }

    [Required]
    public Guid ReceiverId { get; set; }

    public bool IsRead { get; set; } = false;

    public DateTime? ReadAt { get; set; } 
    
    
    
    public  NoteEntity? Note { get; set; }
    public  UserEntity? Receiver { get; set; }
}