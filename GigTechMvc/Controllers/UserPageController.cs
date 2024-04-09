using GigTech.Shared;
using Microsoft.AspNetCore.Mvc;
using GigTechMvc.Models;

namespace GigTechMvc.Controllers
{
    public class UserPageController : Controller
    {
        private readonly GigTechContext _dbContext;

        public UserPageController(GigTechContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserPage()
        {
            var customer = _dbContext.Customers.FirstOrDefault(p => p.CustomerId == 1);
            var products = _dbContext.Products.ToList();

            ViewBag.Customer = customer;
            ViewBag.Products = products;

            return View("/Views/Pages/UserPage.cshtml");
        }

        public IActionResult UserPageEdit()
        {
            var customer = _dbContext.Customers.FirstOrDefault(p => p.CustomerId == 1);
            var products = _dbContext.Products.ToList();

            ViewBag.Customer = customer;
            ViewBag.Products = products;

            return View("/Views/Pages/UserPageEdit.cshtml");
        }

        [HttpPost]
        public IActionResult OnPostSaveChanges(CustomerFormData formData)
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return BadRequest(ModelState);
            }

            // Retrieve the user from the database
            var customer = _dbContext.Customers.FirstOrDefault(p => p.CustomerId == 1);

            if (customer == null)
            {
                // Handle if user is not found
                return NotFound();
            }

            // Update the user with the form data
            customer.FirstName = formData.FirstName;
            customer.LastName = formData.LastName;
            customer.Email = formData.Email;
            customer.PhoneNumber = formData.PhoneNumber;
            customer.Username = formData.Username;

            // Save changes to the database
            _dbContext.SaveChanges();

            return Content("Changes saved successfully!");
        }
    }
}
