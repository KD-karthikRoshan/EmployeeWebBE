using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeWeb.Models
{

    public class Employee

    {

        [Key]


        public int EmployeeId { get; set; }
        [MaxLength(20)]

        public string FirstName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int DepartmentId { get; set; } // Foreign key property


        public Double AnnualSalary { get; set; }





    }
}
