using System.ComponentModel.DataAnnotations;

namespace peackplan.Enums;

public enum AccessLevelEnum
{
    [Display(Name = "خصوصی")]
    Private = 0,

    [Display(Name = "فقط تیم")]
    TeamOnly = 1,

    [Display(Name = "کل شرکت")]
    CompanyWide = 2,

    [Display(Name = "عمومی")]
    Public = 3
}