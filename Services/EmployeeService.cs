namespace blazor.Services;

using System.Threading.Tasks;
using blazor.Data;
using blazor.Models;
using blazor.Services.Responses;
using Microsoft.EntityFrameworkCore;

public interface IEmployeeService
{
    Task<GetEmployeesResponse> GetEmployees();
}

public class EmployeeService : IEmployeeService
{
    private readonly IDbContextFactory<DataContext> _factory;

    public EmployeeService(IDbContextFactory<DataContext> factory)
    {
        _factory = factory;
    }

    public async Task<GetEmployeesResponse> GetEmployees()
    {
        var response = new GetEmployeesResponse();
        try 
        {
            using (var context = _factory.CreateDbContext())
            {
                var employees = await context.Employees.ToListAsync();
                response.Employees = employees;
                response.Message = "Lista de Employees";
                response.StatusCode = 200;
            }
        }
        catch (Exception ex)
        {
            response.Employees = null;
            response.StatusCode = 500;
            response.Message = "Error Retrieving Employees: " + ex.Message;
        }

        return response;
    }
}
