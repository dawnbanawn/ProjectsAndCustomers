using Microsoft.AspNetCore.Mvc;
using ProjectsAndCustomers.Models;
using ProjectsAndCustomers.Models.Entities;

namespace ProjectsAndCustomers.Services.ProjectsService {
    public interface IProjectsService {

        Task AddCustomerAsync(CustomerEntity customer);
        Task AddProjectAsync(ProjectEntity project);

        Task<List<ProjectEntity>> GetAllProjectsWithCustomersAsync();

        Task<ProjectEntity> GetProjectWithCustomerByIdAsync(int id); // For edit page

        Task UpdateProjectAsync(ProjectEntity project);
    }
}
