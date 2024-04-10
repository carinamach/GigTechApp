using GigTech.Shared;
using Microsoft.AspNetCore.Mvc;
using GigTechMvc.Models;
using Microsoft.AspNetCore.Identity;

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
            var orders = _dbContext.

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

        public IActionResult UserPageHistory()
        {
            var customer = _dbContext.Customers.FirstOrDefault(p => p.CustomerId == 1);
            var products = _dbContext.Products.ToList();
            var orders = _dbContext.OrderDetails.ToList();

            ViewBag.Customer = customer;
            ViewBag.Products = products;
            ViewBag.Orders = orders;

            return View("/Views/Pages/UserPageHistory.cshtml");
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

        [HttpPost]
        public IActionResult Deposit(DepositFormData depositData)
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

            // Convert the string vMoney value to an integer
            if (int.TryParse(depositData.vMoney, out int depositValue))
            {
                // Add the deposit value to the current vMoney value
                customer.VMoney += depositValue;
                // Save changes to the database
                _dbContext.SaveChanges();

                return Content("Deposit successful!");
            }
            else
            {
                // Handle the case where the string cannot be parsed as an integer
                // For example, return a BadRequest indicating invalid input
                return BadRequest("Invalid vMoney value");
            }
        }

    }
}
