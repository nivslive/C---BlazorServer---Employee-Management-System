namespace blazor.Services;

using System.Threading.Tasks;
using blazor.Data;
using blazor.Models;
using blazor.Models.DTOs;
using blazor.Services.Responses;
using Microsoft.EntityFrameworkCore;

public interface IEmployeeService
{
    Task<GetEmployeesResponse> GetEmployees();
    Task<AddEmployeeResponse> AddEmployee(AddEmployeeForm employeeForm);
    Task<BaseResponse> DeleteEmployee(Employee employee);
    Task<UpdateEmployeeResponse> UpdateEmployee(Employee employee);
    Task<GetEmployeeResponse> GetEmployee(int id);
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

    public async Task<GetEmployeeResponse> GetEmployee(int id)
    {
        GetEmployeeResponse response = new();
        try
        {
            using var context = _factory.CreateDbContext();
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                response.StatusCode = 200;
                response.Message = "Employee successfully found!";
                response.Employee = employee;
            }
            else
            {
                response.StatusCode = 404; // Not Found
                response.Message = "Employee not found.";
            }
        }
        catch(Exception ex)
        {
            response.StatusCode = 500;
            response.Message = $"An error occurred: {ex.Message}";
        }

        return response;
    }

    public async Task<BaseResponse> DeleteEmployee(Employee employee)
    {
        BaseResponse response = new();
        try
        {
            using var context = _factory.CreateDbContext();
            context.Remove(employee);
            var result = await context.SaveChangesAsync();
            if(result == 1)
            {
                response.StatusCode = 200;
                response.Message = "Employee deleted succesfully!";
            }
            else
            {
                response.StatusCode = 400;
                response.Message = "Error while deleting Employee.";                    
            }
        } catch(Exception ex)
        {
            response.StatusCode = 500;
            response.Message = $"Error Deleting Employee of Id: {employee.Id} | Error message:" + ex.Message;
        }
        return response;
    }


    public async Task<UpdateEmployeeResponse> UpdateEmployee(Employee employee)
    {
        UpdateEmployeeResponse response = new();

        try
        {
            using var context = _factory.CreateDbContext();
            context.Update(employee);
            var result = await context.SaveChangesAsync();
            if(result == 1)
            {
                response.Employee = employee;
                response.StatusCode = 200;
                response.Message = "Employee updated succesfully!";
            }
            else
            {
                response.Employee = null;
                response.StatusCode = 400;
                response.Message = "Error while updating Employee.";                    
            }
        } catch(Exception ex)
        {
            response.StatusCode = 500;
            response.Message = $"Error Updating Employee of Id: {employee.Id} | Error message:" + ex.Message;
        }
        return response;
    }

    public async Task<AddEmployeeResponse> AddEmployee(AddEmployeeForm form)
    {
        var response = new AddEmployeeResponse();
        try
        {
            using (var context = _factory.CreateDbContext())
            {
                context.Add(new Employee
                {
                    Name = form.Name,
                    Position = form.Position,
                    Salary = form.Salary,
                    Type = form.Type,
                    ImgUrl = form.ImgUrl,
                });

                var result = await context.SaveChangesAsync();

                if(result == 1)
                {
                    response.StatusCode = 200;
                    response.Message = "Employee created succesfully!";
                }
                else
                {
                    response.StatusCode = 400;
                    response.Message = "Error while creating Employee.";                    
                }
            }
        } catch(Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Error Retrieving Employees: " + ex.Message;
        }

        return response;
    }
}
