using blazor.Models;

namespace blazor.Services.Responses;


public class GetEmployeeResponse : BaseResponse
{
    public Employee? Employee {get;set;}
}