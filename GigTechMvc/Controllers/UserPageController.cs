using GigTech.Shared;
using Microsoft.AspNetCore.Mvc;
using GigTechMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GigTechMvc.Controllers
{
    public class UserPageController : Controller
    {
        private readonly GigTechContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;


        [Authorize]
        public async Task<string> RetrieveUserId()
        {
            var _currentUser = await _userManager.GetUserAsync(User);
            if (_currentUser == null) { throw new Exception("User not found."); }
            return _currentUser.Id.ToString();
        }

        public UserPageController(GigTechContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //----------------------------------------------
        [Authorize]
        public async Task<IActionResult> UserPage()
        {
            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                // Handle if current user is not found
                return NotFound("Current user not found.");
            }

            // Retrieve the customer associated with the current user's email
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Email == currentUser.Email);

            if (customer == null)
            {
                // Handle if customer is not found
                return NotFound("Customer details not found for the current user.");
            }

            // Check if any of the customer details are missing
            if (string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName) || string.IsNullOrEmpty(customer.PhoneNumber) || string.IsNullOrEmpty(customer.Username) || string.IsNullOrEmpty(customer.ProfileImage))
            {
                // Redirect to detailed registration page
                return RedirectToAction("DetailedRegistration");
            }

            // If all customer details are present, proceed to render the UserPage view
            var products = _dbContext.Products.ToList();
            ViewBag.Products = products;

            return View("/Views/Pages/UserPage.cshtml");
        }

        //-----------------------------------------------------

        public async Task<IActionResult> UserPageEdit()
        {
            var products = _dbContext.Products.ToList();
            ViewBag.Products = products;

            return View("/Views/Pages/UserPageEdit.cshtml");
        }


        public IActionResult UserPageHistory()
        {
            var orders = _dbContext.OrderDetails.ToList();
            var products = _dbContext.Products.ToList();
            var customerIds = _dbContext.Customers.ToList();
            ViewBag.Products = products;
            ViewBag.OrderDetails = orders;
            ViewBag.Customers = customerIds;

            return View("/Views/Pages/UserPageHistory.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> OnPostSaveChanges(CustomerFormData formData)
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return BadRequest(ModelState);
            }

            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                // Handle if current user is not found
                return NotFound("Current user not found.");
            }

            // Retrieve the customer associated with the current user's email
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Email == currentUser.Email);

            if (customer == null)
            {
                // Handle if customer is not found
                return NotFound("Customer details not found for the current user.");
            }

            // Update the customer's details with the form data
            customer.FirstName = formData.FirstName;
            customer.LastName = formData.LastName;
            customer.PhoneNumber = formData.PhoneNumber;
            // Assuming the Email and Username should not be changed based on the form data
            // If they should be updated, you can include them here too

            // Save changes to the database
            _dbContext.SaveChanges();

            return Content("Changes saved successfully!");
        }


        [HttpPost]
        public async Task<IActionResult> Deposit(DepositFormData depositData)
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return BadRequest(ModelState);
            }

            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                // Handle if current user is not found
                return NotFound("Current user not found.");
            }

            // Retrieve the customer associated with the current user's email
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Email == currentUser.Email);

            if (customer == null)
            {
                // Handle if customer is not found
                return NotFound("Customer details not found for the current user.");
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

        [HttpPost]
        public async Task<IActionResult> SaveDetails(CustomerFormData formData)
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return BadRequest(ModelState);
            }

            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                // Handle if current user is not found
                return NotFound("Current user not found.");
            }

            // Retrieve the customer associated with the current user's email
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Email == currentUser.Email);

            if (customer == null)
            {
                // Create a new customer if not found
                customer = new Customer { Email = currentUser.Email };
                _dbContext.Customers.Add(customer);
            }

            // Update customer details with the form data
            customer.FirstName = formData.FirstName;
            customer.LastName = formData.LastName;
            customer.PhoneNumber = formData.PhoneNumber;
            customer.Username = formData.Username;
                                                           // Save changes to the database
            _dbContext.SaveChanges();

            // Redirect to the UserPage after registration
            return RedirectToAction("UserPage");
        }




    }
}
