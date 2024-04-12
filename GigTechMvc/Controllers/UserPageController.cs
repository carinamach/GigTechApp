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
        [Authorize]
        public IActionResult UserPage()
        {
            var products = _dbContext.Products.ToList();
            ViewBag.Products = products;

            return View("/Views/Pages/UserPage.cshtml");
        }

        public async Task<IActionResult> UserPageEdit()
        {
            var products = _dbContext.Products.ToList();
            ViewBag.Products = products;

            return View("/Views/Pages/UserPageEdit.cshtml");
        }


        public IActionResult UserPageHistory()
        {
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
