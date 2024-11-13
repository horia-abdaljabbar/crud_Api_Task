using ApiCrudNew.Data.Models;
using ApiCrudNew.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiCrudNew.DTOs.Department;

namespace ApiCrudNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var departments = context.Departments.Select(
                x => new GetDepartmentsDto()
                {
                    Id = x.Id,
                    Name = x.Name,

                });
            return Ok(departments);
        }


        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var department = context.Departments.Find(id);
            if (department is null)
            {
                return NotFound();
            }

            var departmentDto = new GetDepartmentById
            {
                Id = department.Id,
                Name = department.Name
            };

            return Ok(departmentDto);
        }


        [HttpPost("Create")]
        public IActionResult Create(CreateDepartmentDto departmentDto)
        {
            Department dep = new Department()
            {
                Name = departmentDto.Name
            };

            context.Departments.Add(dep);
            context.SaveChanges();
            return Ok();
        }


        [HttpPut("Update")]
        public IActionResult Update(int id, UpdateDepartmentDto department)
        {
            var current = context.Departments.Find(id);
            if (current is null)
            {
                return NotFound("Department not found");
            }

            current.Name = department.Name;
            context.SaveChanges();

            var departmentDto = new UpdateDepartmentDto
            {
                Name = current.Name
            };

            return Ok(departmentDto);
        }


        [HttpDelete("Remove")]
        public IActionResult Remove(DeleteDepartmentDto departmentDto)
        {

            var department = context.Departments.Find(departmentDto.Id);
            if (department is null)
            {
                return NotFound("Department not found");
            }

            context.Departments.Remove(department);
            context.SaveChanges();

            return Ok("the department deleted successfully");  
        }

    }

}
