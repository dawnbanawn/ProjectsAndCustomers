using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Add this namespace
using Microsoft.EntityFrameworkCore;
using ProjectsAndCustomers.Models.Entities;

namespace ProjectsAndCustomers.Data {
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> // Change DbContext to IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
    }
}
