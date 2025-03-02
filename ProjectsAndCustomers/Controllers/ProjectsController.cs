using Microsoft.AspNetCore.Mvc;
using ProjectsAndCustomers.Data;
using ProjectsAndCustomers.Models;
using ProjectsAndCustomers.Models.Entities;

namespace ProjectsAndCustomers.Controllers {
    public class ProjectsController : Controller {

        private readonly ApplicationDbContext dbContext; // the variable we will use

        public ProjectsController(ApplicationDbContext dbContext) { // inject dbcontext whit this constructor
            this.dbContext = dbContext;
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


            // Create a customer instance with incoming customer name
            var customer = new CustomerEntity {
                CustomerName = customerName
            };

            // Add and save the customer to customers table
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();

            // Create a new instance using the model, and fill with the incoming data
            var project = new ProjectEntity {
                Title = viewModel.Title,
                Description = viewModel.Description,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                CustomerId = customer.Id // Use customer instance id here to connect

            };
            await dbContext.Projects.AddAsync(project); // Send the data to the db.
            await dbContext.SaveChangesAsync(); // Actually saaving the changes

            return View();
        }
    }
}
