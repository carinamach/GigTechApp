using GigTech.Shared;
using Microsoft.AspNetCore.Mvc;
using GigTechMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;

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
                // Handle if user is not found
                return RedirectToAction("DetailedRegistration", "UserPage");
            }

            // Retrieve the customer associated with the current user's email
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Email == currentUser.Email);

            if (customer == null)
            {
                // Redirect to detailed registration page if customer not found
                return RedirectToAction("DetailedRegistration", "UserPage");
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


        [Authorize]
        public async Task<IActionResult> DetailedRegistration()
        {
            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                // If the current user is null, redirect to the registration page
                return RedirectToAction("Registration", "Account"); // Adjust the redirection URL as needed
            }

            // Retrieve the customer associated with the current user's email
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Email == currentUser.Email);

            if (customer == null)
            {
                // If the customer is not found, it means detailed registration has not been completed
                return View("/Views/Pages/DetailedRegistration.cshtml"); // Redirect to registration page
            }
            else
            {
                // If the customer is found, check if it has a first name
                if (string.IsNullOrEmpty(customer.FirstName))
                {
                    // If the customer does not have a first name, redirect to the detailed registration page
                    return View("/Views/Pages/DetailedRegistration.cshtml"); // Redirect to detailed registration page
                }
                else
                {
                    // If the customer has a first name, it means detailed registration has already been completed
                    return Content("You have already registered this information.");
                }
            }
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
            customer.Username = formData.Username;
            customer.ProfileImage = formData.ProfileImage;

            // Save changes to the database
            _dbContext.SaveChanges();

            return Content("Changes saved successfully!");
        }

        [HttpPost]
        public async Task<IActionResult> OnRegistrationSaveChanges(RegisterFormData formData)
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

            // Check if the current user's email exists in the Customers table
            var existingCustomer = _dbContext.Customers.FirstOrDefault(c => c.Email == currentUser.Email);

            if (existingCustomer == null)
            {
                // Create a new customer object and populate its properties with the form data
                var newCustomer = new Customer
                {
                    FirstName = formData.FirstName,
                    LastName = formData.LastName,
                    PhoneNumber = formData.PhoneNumber,
                    Username = formData.Username,
                    ProfileImage = formData.ProfileImage,
                    Email = currentUser.Email, // Use the email of the current user
                    PasswordHash = formData.PasswordHASH, // Use the email of the current user
                    ProductId = formData.ProductId,
                    CustomerId = formData.CustomerId, // Use the email of the current user
                    VMoney = formData.vMoney // Use the email of the current user

                };

                try
                {
                    // Add the new customer to the database
                    _dbContext.Customers.Add(newCustomer);
                    await _dbContext.SaveChangesAsync(); // Save changes asynchronously

                    return Content("New customer created successfully!");
                }
                catch (Exception ex)
                {
                    // Handle exceptions, e.g., database errors
                    return StatusCode(500, $"An error occurred while creating the new customer: {ex.Message}. Inner exception: {ex.InnerException?.Message}");
                }

            }
            else
            {
                // Customer with the same email already exists
                return Content("Customer with this email already exists.");
            }
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

    }
}
