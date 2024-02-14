using EmployeeWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmployeeWeb.Database
{
    public class EmployeeWebDbContext : DbContext
    {
        internal object employee;

        public EmployeeWebDbContext(DbContextOptions options): base(options) {


        }

        public DbSet<Employee> employees { get; set; }
       public DbSet<Department> departments { get; set; }

    }
}
