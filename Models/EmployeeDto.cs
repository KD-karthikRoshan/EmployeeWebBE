using System.ComponentModel.DataAnnotations;

namespace EmployeeWeb.Models
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  DateOfBirth { get; set; }
        public Double AnnualSalary { get; set; }
        public String DepartmentName { get; set; }

        public int DepartmentId { get; set; }
        public String Location { get; set; }

    }
}
