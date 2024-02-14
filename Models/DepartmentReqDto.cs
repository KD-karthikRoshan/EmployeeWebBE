using System.ComponentModel.DataAnnotations;

namespace EmployeeWeb.Models
{
    public class DepartmentReqDto
    {
        public String DepartmentName { get; set; }

        [MaxLength(20)]

        public String Location { get; set; }
    }
}
