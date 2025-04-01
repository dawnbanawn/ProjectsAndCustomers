using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsAndCustomers.Data;
using ProjectsAndCustomers.Models;
using ProjectsAndCustomers.Models.Entities;

namespace ProjectsAndCustomers.Services.ProjectsService {
    public class ProjectsService : IProjectsService { // This uses the interface

        private readonly ApplicationDbContext dbContext; // the variable we will use to reach dbcontext
        public ProjectsService(ApplicationDbContext dbContext) { // inject dbcontext whit this constructor
            this.dbContext = dbContext;
        }

        // Copilot hjälpte mig med repository implementation nedan,
        // Det är bara metoder flyttade från kontrollern för att separera var man når dbcontext, som innan vad i kontrollern.
        public async Task AddCustomerAsync(CustomerEntity customer) {
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddProjectAsync(ProjectEntity project) {
            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<ProjectEntity>> GetAllProjectsWithCustomersAsync() {
            return await dbContext.Projects
                                  .Include(p => p.Customer)
                                  .ToListAsync();

        }
        public async Task<ProjectEntity> GetProjectWithCustomerByIdAsync(int id) {
            return await dbContext.Projects
                                  .Include(p => p.Customer)
                                  .FirstOrDefaultAsync(p => p.Id == id);

        }
        public async Task UpdateProjectAsync(ProjectEntity project) {
            dbContext.Projects.Update(project);
            await dbContext.SaveChangesAsync();
        }
        //Copilot helped with some of the codes when bug fixing
        public async Task<bool> DeleteProjectAsync(int projectId) {
            //Console.WriteLine($"Attempting to delete project with ID: {projectId}");
            var project = await dbContext.Projects.FindAsync(projectId);

            if (project == null) {
                Console.WriteLine($"Project with ID {projectId} not found.");
                return false;
            }

            try {
                dbContext.Projects.Remove(project);
                await dbContext.SaveChangesAsync();
                Console.WriteLine($"Project with ID {projectId} deleted successfully.");
                return true;
            } catch (Exception ex) {
                Console.WriteLine($"Error deleting project: {ex.Message}");
                return false;
            }
        }


    }
}
