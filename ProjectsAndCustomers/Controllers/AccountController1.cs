using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProjectsAndCustomers.Controllers {
    public class AccountController : Controller {

        // Method for signing out, copilot helped me with this when bug fixing.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("List", "Projects"); // redirect to projects page
        }
    }

}
