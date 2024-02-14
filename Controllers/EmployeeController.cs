using EmployeeWeb.Database;
using EmployeeWeb.Models;
using EmployeeWeb.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace EmployeeWeb.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeWebDbContext _employeeWebDbContext;
        private readonly EmployeeServices _employeeServices;

        public EmployeeController(EmployeeWebDbContext employeeWebDbContext, EmployeeServices employeeServices)
        {
            _employeeWebDbContext = employeeWebDbContext;
            _employeeServices = employeeServices;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> createEmployee([FromBody] EmployeeReqDto emp)
        {
            int max_length = 20;
           
            if (string.IsNullOrWhiteSpace(emp.FirstName) || string.IsNullOrWhiteSpace(emp.LastName))
            {
                return BadRequest("Invalid First/ Last name");
            }


            if(emp.FirstName.Length > max_length || emp.LastName.Length>max_length) {
            
              return BadRequest("Frist / last name length is high");
            }
            if (emp == null)
            {
                return BadRequest("Invalid employee data");
            }

            if (emp.DateOfBirth > DateTime.Today)
            {
                return BadRequest("Invalid DateofBirth in employee data (Date/Time is in future)");
            }

            Employee employee = await _employeeServices.createEmployee(emp);
            if (employee != null)
            {
                return Ok(employee);
            } else
            {
                return NotFound("Given department Not Found");
            }
        }

        [HttpGet]
        [Route("get_by_id/{Id}")]
        public async Task<IActionResult> getById(int Id)
        {
            var emp = await  _employeeServices.getById(Id);

            if (emp != null)
                return Ok(emp);
            else
                return NotFound("Employee Not Found");
        }

        [HttpGet]
        [Route("get_all_employee")]
        public async Task<IActionResult> getAll()
        {
            var employees = await  _employeeServices.getAll();

            return Ok(employees);
        }

        [HttpPut]
        [Route("update_employee/{Id}")]
        public async Task<IActionResult> updateEmployee(int Id, [FromBody] EmployeeReqDto emp)
        {

            int max_length = 20;

            if (string.IsNullOrWhiteSpace(emp.FirstName) || string.IsNullOrWhiteSpace(emp.LastName))
            {
                return BadRequest("Invalid First/ Last name");
            }


            if (emp.FirstName.Length > max_length || emp.LastName.Length > max_length)
            {

                return BadRequest("Frist / last name length is high");
            }
            var existingEmp = await _employeeServices.updateEmployee(Id, emp);

            if (existingEmp != null)
            {
                return Ok(existingEmp);
            }
            else
            {
                return NotFound("Employee Not Found");
            }
        }

        [HttpDelete]
        [Route("delete_by_id/{Id}")]
        public async Task<IActionResult> deleteEmployee(int Id)
        {
            var emp = await _employeeServices.deleteEmployee(Id);
            if (emp != null)
            {
                return Ok(emp);
            }
            else
            {
                return NotFound("Employee Not Found");
            }
        }
    }
}
