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