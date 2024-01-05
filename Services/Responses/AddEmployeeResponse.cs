using blazor.Models;

namespace blazor.Services.Responses;


public class AddEmployeeResponse : BaseResponse
{
    public Employee? Employee {get;set;}
}