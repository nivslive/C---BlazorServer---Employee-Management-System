using blazor.Models;

namespace blazor.Services.Responses;


public class UpdateEmployeeResponse : BaseResponse
{
    public Employee? Employee {get;set;}
}