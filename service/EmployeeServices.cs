using EmployeeWeb.Database;
using EmployeeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWeb.service
{
    public class EmployeeServices
    {
        private readonly EmployeeWebDbContext _employeeWebDbContext;

        public EmployeeServices(EmployeeWebDbContext employeeWebDbContext)
        {
            _employeeWebDbContext = employeeWebDbContext;
        }

        public async Task<List<EmployeeDto>> getAll()
        {
            var employees = await _employeeWebDbContext.employees
                                .OrderBy(e => e.LastName)
                                .ToListAsync();
            var employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                Department department = _employeeWebDbContext.departments.FirstOrDefault(d => d.DepartmentId == employee.DepartmentId);
                EmployeeDto emp = convertToResponseDto(employee, department);
                employeeDtos.Add(emp);
            }
            return employeeDtos;
        }

        public async Task<Employee> createEmployee(EmployeeReqDto emp)
        {
            try
            {
                Employee employee = convertDto(emp);

                await _employeeWebDbContext.employees.AddAsync(employee);
                await _employeeWebDbContext.SaveChangesAsync();
                return employee;
            } catch
            {
                return null;
            }
        }
        public EmployeeDto convertToResponseDto(Employee employee, Department department)
        {
            EmployeeDto employeeDto = new EmployeeDto();
            employeeDto.EmployeeId = employee.EmployeeId;
            employeeDto.FirstName = employee.FirstName;
            employeeDto.LastName = employee.LastName;
            employeeDto.DateOfBirth = employee.DateOfBirth.ToShortDateString(); ;
            employeeDto.DepartmentName = department.DepartmentName;
            employeeDto.Location = department.Location;
            employeeDto.AnnualSalary = employee.AnnualSalary;
            employeeDto.DepartmentId = department.DepartmentId;
            return employeeDto;
        }

        public Employee convertDto(EmployeeReqDto employeeReqDto)
        {
            Employee employee = new Employee();
            employee.FirstName = employeeReqDto.FirstName;
            employee.LastName = employeeReqDto.LastName;
            employee.DateOfBirth = employeeReqDto.DateOfBirth;
            employee.DepartmentId = employeeReqDto.DepartmentId;
            employee.AnnualSalary = employeeReqDto.AnnualSalary;
            return employee;
        }
        public async Task<Employee> getById(int Id)
        {
            var emp = await _employeeWebDbContext.employees.FirstOrDefaultAsync(x => x.EmployeeId == Id);
            return emp;
        }
        public async Task<List<Employee>> getByDeptId(int deptId)
        {
            var employees = await _employeeWebDbContext.employees
                .Where(x => x.DepartmentId == deptId)
                .ToListAsync();
            return employees;
        }


        public async Task<Employee> updateEmployee(int Id, EmployeeReqDto emp)
        {
            var existingEmp = await _employeeWebDbContext.employees.FirstOrDefaultAsync(x => x.EmployeeId == Id);

            if (existingEmp != null)
            {
                existingEmp.FirstName = emp.FirstName;
                existingEmp.LastName = emp.LastName;
                existingEmp.DateOfBirth = emp.DateOfBirth;
                existingEmp.DepartmentId = emp.DepartmentId;
                existingEmp.AnnualSalary = emp.AnnualSalary;
                _employeeWebDbContext.employees.Update(existingEmp);
                await _employeeWebDbContext.SaveChangesAsync();
                return existingEmp;
            }
            else
            {
                return null;
            }
        }
        public async Task<Employee> deleteEmployee(int Id)
        {
            var emp = await _employeeWebDbContext.employees.FirstOrDefaultAsync(x => x.EmployeeId == Id);
            if (emp != null)
            {
                _employeeWebDbContext.employees.Remove(emp);
                await _employeeWebDbContext.SaveChangesAsync();
                return emp;
            }
            else
            {
                return null;
            }
        }
    }
}
