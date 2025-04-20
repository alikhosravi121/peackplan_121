using System.ComponentModel.DataAnnotations;

namespace peackplan.Enums;

public enum  OkrStatusEnum
{
    [Display(Name = "شروع نشده")]
    NotStarted = 0,

    [Display(Name = "در حال انجام")]
    InProgress = 1,

    [Display(Name = "تکمیل شده")]
    Completed = 2,

    [Display(Name = "لغو شده")]
    Cancelled = 3
} 
 