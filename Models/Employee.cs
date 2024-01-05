using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace blazor.Models;


public class Employee 
{
    public int Id {get; set;}
    public string? Name {get; set;}
    
    public string ImgUrl {get; set;}
    public decimal Salary {get; set;}
    public EmployeeType Type {get; set;}

    public Position Position {get; set;}
}


public static class EnumExtentions
{
    public static string? GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<DisplayAttribute>()?.GetName();
    }
}