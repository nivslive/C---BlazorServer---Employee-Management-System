using System.ComponentModel.DataAnnotations;

public enum EmployeeType
{
    [Display(Name = "Freelance")]
    Freelance,

    [Display(Name = "Casual")]
    Casual,


    [Display(Name = "Part Time")]
    PartTime,

    [Display(Name = "FullTime")]
    FullTime
    
}