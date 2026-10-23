using EmployeeSecurityAPI.Attributes;
using EmployeeSecurityAPI.Models;
using EmployeeSecurityAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSecurityAPI.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    [BasicAuthentication]
    [MyAuthorize("Admin", "Superadmin")]
    public ActionResult<IEnumerable<Employee>> GetAll()
    {
        return Ok(_employeeService.GetAll());
    }

    [HttpGet("male")]
    [BasicAuthentication]
    [MyAuthorize("Admin")]
    public ActionResult<IEnumerable<Employee>> GetMale()
    {
        return Ok(_employeeService.GetByGender("Male"));
    }

    [HttpGet("female")]
    [BasicAuthentication]
    [MyAuthorize("Superadmin")]
    public ActionResult<IEnumerable<Employee>> GetFemale()
    {
        return Ok(_employeeService.GetByGender("Female"));
    }

    [HttpGet("{id:int}")]
    [BasicAuthentication]
    [MyAuthorize("Admin", "Superadmin")]
    public ActionResult<Employee> GetById(int id)
    {
        var employee = _employeeService.GetById(id);
        return employee is null ? NotFound() : Ok(employee);
    }

    [HttpGet("public")]
    public IActionResult GetPublic()
    {
        return Ok(new { Company = "Example Company", Description = "Company information" });
    }
}
