using System.ComponentModel.DataAnnotations;

namespace EmployeeWeb.Models
{
    public class Department
    {
        [Key]
     public int DepartmentId { get; set; }
        
        [MaxLength(20)]
     public String DepartmentName { get; set; }
       
        [MaxLength(20)]

        public String Location { get; set;}



    }
}
