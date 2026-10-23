namespace EmployeeSecurityAPI.Models;

public class Employee
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Department { get; init; }
    public required string Gender { get; init; }
    public decimal Salary { get; init; }
}
