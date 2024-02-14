using EmployeeWeb.Database;
using EmployeeWeb.Models;
using EmployeeWeb.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWeb.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly EmployeeWebDbContext _dbContext;
        private readonly DepartmentServices _departmentservices;


        public DepartmentController(EmployeeWebDbContext dbContext, DepartmentServices departmentservices)
        {
            _dbContext = dbContext;
            _departmentservices = departmentservices;
        }


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> createDepartment([FromBody] DepartmentReqDto dep)
        {
            int max_length = 20;

            if (dep == null)
                return BadRequest("Invalid employee data");
            if (string.IsNullOrWhiteSpace(dep.DepartmentName) || string.IsNullOrWhiteSpace(dep.Location))
            {
                return BadRequest("Invalid Department/ Location name");
            }
            if (dep.DepartmentName.Length > max_length || dep.Location.Length > max_length)
            {

                return BadRequest("DepartmentName / Location length is high");
            }
            Department department = await _departmentservices.createDepartment(dep);
            if (department!=null)
            {
                return Ok(department);
            }
            else
            {
                return BadRequest("Duplicate department name");
            }

        }


        [HttpGet]
        [Route("get_by_id/{Id}")]
        public async Task<IActionResult> getById(int Id)
        {
            var dep =await _departmentservices.getById(Id);

            if (dep != null)
                return Ok(dep);
            else
                return NotFound("Department Not Found");
        }

        [HttpGet]
        [Route("get_all_department")]
        public async Task<IActionResult> getAll()
        {
            var dep = await _departmentservices.getAll();

            return Ok(dep);
        }

        [HttpPut]
        [Route("update_department/{Id}")]
        public async Task<IActionResult> updateEmployee(int Id, [FromBody] DepartmentReqDto dep)
        {
            int max_length = 20;
            if (dep == null)
                return BadRequest("Invalid employee data");
            if (string.IsNullOrWhiteSpace(dep.DepartmentName) || string.IsNullOrWhiteSpace(dep.Location))
            {
                return BadRequest("Invalid Department/ Location name");
            }

            if (dep.DepartmentName.Length > max_length || dep.Location.Length > max_length)
            {

                return BadRequest("DepartmentName / Location length is high");
            }
            var dep1 = await _departmentservices.updateDepartment(Id,dep);

            if (dep1 != null)
            {
                return Ok(dep1);
            }
            else
            {
                return NotFound("Department Not Found");
            }
        }

        [HttpDelete]
        [Route("delete_by_id/{Id}")]
        public async Task<IActionResult> deleteDepartment(int Id)
        {
            var dep = await _departmentservices.deleteDepartment(Id);
            if (dep != null)
            {
                return Ok(dep);
            }
            else
            {
                return BadRequest("Department Not Found/ There are employees present in the department");
            }
        }
    }


}








