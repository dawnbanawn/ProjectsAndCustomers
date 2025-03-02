using Microsoft.EntityFrameworkCore;
using ProjectsAndCustomers.Models.Entities;

namespace ProjectsAndCustomers.Data {
    public class ApplicationDbContext : DbContext{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            
        }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
    }
}
