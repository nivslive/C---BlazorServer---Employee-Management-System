using System.ComponentModel.DataAnnotations;

public enum Position
{
    [Display(Name = "CEO")]
    CEO,

    [Display(Name = "CTO")]
    CTO,


    [Display(Name = "CFO")]
    CFO,

    [Display(Name = "Accountant")]
    Accountant,

    [Display(Name = "HR Manager")]
    HRManager,
    
    [Display(Name = "Marketing Manager")]
    MarketingManager,

    
}