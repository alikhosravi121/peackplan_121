using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Extensions;
using peackplan.Entities;
using peackplan.Enums;

namespace peackplan.Dtos;

public class PrimaryTaskCreate
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
}

public class PrimaryTaskResponse{ 
    public Guid Id { get; set; }
 
    public string Title { get; set; } = default!;

    public string? Description { get; set; }
     
    public OkrStatusEnum Status { get; set; } 
    public string StatusName => Status.ToString(); // "Completed"
    public string StatusDisplay => Status.GetDisplayName(); // "تکمیل شده"
    
    public AccessLevelEnum AccessLevel { get; set; }
    public string AccessLevelName => AccessLevel.ToString();
    public string AccessLevelDisplay => AccessLevel.GetDisplayName();

    

    public Guid? ParentTaskId { get; set; }
    
    public Guid? AvatarId { get; set; }

    public DateTime? DueDate { get; set; } 
    public string? Tags { get; set; }
    public string? NameManager { get; set; } 
    public string? AvatarManager{get;set;} 
    public Guid ManagerId { get; set; }  

}

public class PrimaryTaskUpdate
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string? Title { get; set; } = default!;

    public string? Description { get; set; }
    [Required]
    public OkrStatusEnum? Status { get; set; }
    [Required]
    public AccessLevelEnum? AccessLevel { get; set; }  // enum تعریف‌شده برای دسترسی‌ها

    public Guid? ParentTaskId { get; set; }
    
    public Guid? AvatarId { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Tags { get; set; } // کاما جدا یا json string

    [Required]
    public Guid? ManagerId { get; set; }  
}