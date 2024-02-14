using EmployeeWeb.Database;
using EmployeeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWeb.service
{
    public class DepartmentServices
    {
        private readonly EmployeeWebDbContext _employeeWebDbContext;
        private readonly EmployeeServices _employeeServices;

        public DepartmentServices(EmployeeWebDbContext employeeWebDbContext, EmployeeServices employeeServices)
        {
            _employeeWebDbContext = employeeWebDbContext;
            _employeeServices = employeeServices;

        }

        public async Task<List<Department>> getAll()
        {
            return await _employeeWebDbContext.departments.ToListAsync();
        }

        public async Task<Department> createDepartment(DepartmentReqDto dep)
        {
            Department departmentFromDb = await _employeeWebDbContext.departments.FirstOrDefaultAsync(x => x.DepartmentName == dep.DepartmentName);
            if (departmentFromDb == null)
            {
                Department department = convertDto(dep);
                await _employeeWebDbContext.departments.AddAsync(department);
                await _employeeWebDbContext.SaveChangesAsync();
                return department;
            } else
            {
                return null;
            }
        }

        public Department convertDto(DepartmentReqDto departmentReqDto)
        {
            Department department = new Department();
            department.DepartmentName = departmentReqDto.DepartmentName;
            department.Location = departmentReqDto.Location;
            return department;
        }

        public async Task<Department> getById(int Id)
        {
            var dep = await _employeeWebDbContext.departments.FirstOrDefaultAsync(x => x.DepartmentId == Id);
            return dep;
        }


        public async Task<Department> updateDepartment(int Id, DepartmentReqDto dep)
        {
            var existingDep = await _employeeWebDbContext.departments.FirstOrDefaultAsync(x => x.DepartmentId == Id);

            if (existingDep != null)
            {
                existingDep.DepartmentName = dep.DepartmentName;
                existingDep.Location = dep.Location;
                _employeeWebDbContext.departments.Update(existingDep);
                await _employeeWebDbContext.SaveChangesAsync();
                return existingDep;
            }
            else
            {
                return null;
            }
        }
        public async Task<Department> deleteDepartment(int Id)
        {

            var dep = await _employeeWebDbContext.departments.FirstOrDefaultAsync(x => x.DepartmentId == Id);
            if (dep != null)
            {
                List<Employee> listOfEmployees = await _employeeServices.getByDeptId(Id);
                if (!listOfEmployees.Any()) {
                    _employeeWebDbContext.departments.Remove(dep);
                    await _employeeWebDbContext.SaveChangesAsync();
                    return dep;
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }

}

