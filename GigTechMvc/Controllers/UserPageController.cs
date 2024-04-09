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
        public IActionResult OnPostSaveChanges(string firstName, string lastName, string email, string phoneNumber, string password, string username)
        {
            // Process form submission
            return Content("Changes saved successfully!");
        }
    }
}
