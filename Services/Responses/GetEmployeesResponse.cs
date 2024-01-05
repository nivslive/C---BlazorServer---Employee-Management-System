using blazor.Models;

namespace blazor.Services.Responses;


public class GetEmployeesResponse : BaseResponse
{
    public List<Employee>? Employees {get;set;}
}