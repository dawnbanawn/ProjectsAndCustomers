using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsAndCustomers.Data;
using ProjectsAndCustomers.Models;
using ProjectsAndCustomers.Models.Entities;
using ProjectsAndCustomers.Services.ProjectsService;
using Microsoft.AspNetCore.Authorization;



namespace ProjectsAndCustomers.Controllers {
    public class ProjectsController : Controller {

        //private readonly ApplicationDbContext dbContext; // the variable we will use to reach dbcontext
        private readonly IProjectsService projectsService;
        public ProjectsController(ApplicationDbContext dbContext, IProjectsService projectsService) { // inject dbcontext whit this constructor
            //this.dbContext = dbContext;
            this.projectsService = projectsService;
        }


        // Get method for getting the add page view, /projects/add 
        [HttpGet]
        public IActionResult Add() {
            return View();
        }

        // Post method for the submit button in the form with post method, asyncronous because of the db handling
        // Copilot hjälpte mig lista ut att jag kunde ta med customerName bredvid viewModel argumentet, och bara duplicera processen.
        [HttpPost]
        public async Task<IActionResult> Add(AddProjectViewModel viewModel, string customerName) { // Post button returns form data bound to this model


            // Create a customer instance with incoming customer name from form
            var customer = new CustomerEntity {
                CustomerName = customerName
            };

            // Add and save the customer to customers table
            //await dbContext.Customers.AddAsync(customer);
            //await dbContext.SaveChangesAsync();
            await projectsService.AddCustomerAsync(customer); // Save customer at the repository

            // Create a new instance using the model, and fill with the incoming data
            var project = new ProjectEntity {
                Title = viewModel.Title,
                Description = viewModel.Description,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                Budget = viewModel.Budget,
                CustomerId = customer.Id // Use customer instance id here to connect

            };
            //await dbContext.Projects.AddAsync(project); // Send the data to the db.
            //await dbContext.SaveChangesAsync(); // Actually saaving the changes
            await projectsService.AddProjectAsync(project);


            return RedirectToAction("List", "Projects"); // Go to list page after adding project.
        }

        //[Authorize]
        // Get list from database
        [HttpGet]
        public async Task<IActionResult> List() {
            // Get data from projects entity, and include Customer (Copilot hjälpte mig komma på include här)
            //var projects = await dbContext.Projects
            //                               .Include(p => p.Customer)
            //                               .ToListAsync();
            var projects = await projectsService.GetAllProjectsWithCustomersAsync(); // Get it from repository instead.
            return View(projects); // return the view with the data
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id) {
        //    // Get only one project with incoming id, som sagt ovan så hjälpte copilot mig med include. KLurigt med två entiteter.
        //    //var project = await dbContext.Projects
        //    //                                 .Include(p => p.Customer)
        //    //                                 .FirstOrDefaultAsync(p => p.Id == id);
        //    var project = await projectsService.GetProjectWithCustomerByIdAsync(id);

        //    return View(project); // Return the edit view with the project

        //}

        // Get for getting the Edit partial view
        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            var project = await projectsService.GetProjectWithCustomerByIdAsync(id);

            if (project == null) {
                return NotFound();
            }

            return PartialView("_EditProjectModal", project); // Return the modal partial with the project data
        }


        // Method to recieve the form data from the edit page
        [HttpPost]
        public async Task<IActionResult> Edit(ProjectEntity viewModel) {
            var project = await projectsService.GetProjectWithCustomerByIdAsync(viewModel.Id);

            if (project == null) {
                return NotFound();
            }

            // Check and initialize the Customer property if null
            project.Customer ??= new CustomerEntity();

            // Map the updated values
            project.Title = viewModel.Title;
            project.Description = viewModel.Description;
            project.StartDate = viewModel.StartDate;
            project.EndDate = viewModel.EndDate;
            project.Budget = viewModel.Budget;
            project.Customer.CustomerName = viewModel.Customer.CustomerName;

            // Save changes
            await projectsService.UpdateProjectAsync(project);

            return RedirectToAction("List", "Projects");
        }

        // Delete a project
        [HttpPost]
        public async Task<IActionResult> Delete([FromQuery] int id) {
            if (id <= 0) {
                return BadRequest("Invalid project ID.");
            }

            var isDeleted = await projectsService.DeleteProjectAsync(id);

            if (isDeleted) {
                return Ok(); // Deletion was successful
            }

            return BadRequest("Failed to delete the project."); // Deletion failed
        }




    }
}
