using ApiCrudNew.Data.Models;
using ApiCrudNew.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiCrudNew.DTOs.Employee;
using Mapster;

namespace ApiCrudNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var employees = context.Employees.ToList();
            var response = employees.Adapt<IEnumerable<GetAllEmployeesDto>>();
            return Ok(response);
        }


        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            var response = employee.Adapt<GetEmployeeByIdDto>();

            return Ok(response);
        }


        [HttpPost("Create")]
        public IActionResult Create(CreateEmployeeDto employeeDto)
        {
            var employee = employeeDto.Adapt<Employee>();
            context.Employees.Add(employee);
            context.SaveChanges();
            return Ok(employee);
        }


        [HttpPut("Update")]
        public IActionResult Update(int id, UpdateEmployeeDto employeeDto)
        {
            var current = context.Employees.Find(id);
            if (current is null)
            {
                return NotFound("employee not found");

            }
            current.Name = employeeDto.Name;
            current.Description = employeeDto.Description;
            current.DepartmentId = employeeDto.DepartmentId;
            current.Adapt(employeeDto);

            context.SaveChanges();

            var updatedEmployeeDto = current.Adapt<UpdateEmployeeDto>();  
            return Ok(updatedEmployeeDto); 
        }


        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("employee not found");
            }
            context.Employees.Remove(employee);
            employee.Adapt<DeleteEmployeeDto>();
            context.SaveChanges();
            return Ok("the employee deleted successfully");
        }
    }
}
