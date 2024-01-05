using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using blazor.Models;
using Bogus;
namespace blazor.Data
{
    public class DataContext : IdentityDbContext
    {
        public DbSet<Employee> Employees {get; set;}
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Employee>().HasData(GetEmployees());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            // other configurations...
        }

        private List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            var faker = new Faker("en");

            for (int i = 0; i < 100; i++)
            {
                var employee = new Employee
                {
                    Id = -(i + 1),
                    ImgUrl = faker.Internet.Avatar(),
                    Name = faker.Name.FullName(),
                    Salary = GetRandomSalary(),
                    Type = GetRandomEmployeeType(),
                    Position = GetRandomEmployeePosition()
                };

                employees.Add(employee);
            }

            return employees;
        }


        private Position GetRandomEmployeePosition()
        {
            var random = new Random();
            var types = Enum.GetValues(typeof(Position));

            return (Position)types.GetValue(random.Next(types.Length));
        }

        private EmployeeType GetRandomEmployeeType()
        {
            var random = new Random();
            var types = Enum.GetValues(typeof(EmployeeType));

            return (EmployeeType)types.GetValue(random.Next(types.Length));
        }

        private decimal GetRandomSalary()
        {
            var random = new Random();
            decimal salary = random.Next(300000, 10000000);        
            return salary;
        }

    }
}