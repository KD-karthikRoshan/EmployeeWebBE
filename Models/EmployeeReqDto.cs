using System.ComponentModel.DataAnnotations;

namespace EmployeeWeb.Models
{
    public class EmployeeReqDto
    {

        public string FirstName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int DepartmentId { get; set; }

        public Double AnnualSalary { get; set; }

    }
}
