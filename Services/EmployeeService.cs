using EmployeeSecurityAPI.Models;

namespace EmployeeSecurityAPI.Services;

public class EmployeeService
{
    private readonly Employee[] _employees =
    [
        new() { Id = 1, Name = "Alex Johnson", Department = "Engineering", Gender = "Male", Salary = 85000m },
        new() { Id = 2, Name = "Maria Garcia", Department = "Finance", Gender = "Female", Salary = 79000m },
        new() { Id = 3, Name = "Daniel Kim", Department = "Sales", Gender = "Male", Salary = 72000m },
        new() { Id = 4, Name = "Emma Wilson", Department = "Human Resources", Gender = "Female", Salary = 76000m },
        new() { Id = 5, Name = "Michael Brown", Department = "Marketing", Gender = "Male", Salary = 68000m },
        new() { Id = 6, Name = "Olivia Davis", Department = "Engineering", Gender = "Female", Salary = 92000m },
        new() { Id = 7, Name = "James Miller", Department = "Operations", Gender = "Male", Salary = 81000m },
        new() { Id = 8, Name = "Sophia Martinez", Department = "Sales", Gender = "Female", Salary = 74000m },
        new() { Id = 9, Name = "Robert Taylor", Department = "Finance", Gender = "Male", Salary = 88000m },
        new() { Id = 10, Name = "Isabella Anderson", Department = "Marketing", Gender = "Female", Salary = 70000m }
    ];

    public IEnumerable<Employee> GetAll() => _employees;

    public IEnumerable<Employee> GetByGender(string gender) =>
        _employees.Where(employee => string.Equals(employee.Gender, gender, StringComparison.Ordinal));

    public Employee? GetById(int id) => _employees.FirstOrDefault(employee => employee.Id == id);
}
