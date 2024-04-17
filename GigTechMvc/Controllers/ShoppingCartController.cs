using GigTech.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GigTechMvc.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly GigTechContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
       
        public IActionResult Index()
        {
            return View();
        }
        public ShoppingCartController(GigTechContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<String> RetrieveUserId()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var currentUserId = currentUser?.Id;
            return currentUserId;
        }
        
        [Authorize]
        public IActionResult ShoppingCart()
        {
            var CustomerId = _dbContext.ShoppingCart.FirstOrDefault()?.CustomerId;
            var ProductId = _dbContext.ShoppingCart.FirstOrDefault()?.ProductId;
            var ProductName = _dbContext.ShoppingCart.FirstOrDefault()?.ProductName;
            var ProductPrice = _dbContext.ShoppingCart.FirstOrDefault()?.ProductPrice;

            ViewBag.CustomerId = CustomerId;
            ViewBag.ProductId = ProductId;
            ViewBag.ProductName = ProductName;
            ViewBag.ProductPrice = ProductPrice;

            ViewBag.ShoppingCart = _dbContext.ShoppingCart.ToList();

            return View("/Views/Pages/ShoppingCart.cshtml");
        }

        [Authorize]
        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            // Retrieve the shopping cart item by product id
            var cartItem = _dbContext.ShoppingCart.FirstOrDefault(item => item.ProductId.ToString() == productId.ToString());

            if (cartItem != null)
            {
                // Remove the item from the shopping cart
                _dbContext.ShoppingCart.Remove(cartItem);
                _dbContext.SaveChanges();
            }

            // Redirect back to the shopping cart page
            return RedirectToAction("ShoppingCart");
        }








        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Pay()
        {
            // Retrieve the current user's ID
            var userId = await _userManager.GetUserAsync(User);

            // Retrieve the customer from the database using the user's email
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == userId.Email);
            if (customer == null)
            {
                return NotFound("User not found.");
            }

            // Retrieve shopping cart items for the current user
            var shoppingCartItems = _dbContext.ShoppingCart.Where(item => item.CustomerId.ToString() == customer.CustomerId.ToString()).ToList();


            // Add games from the shopping cart to the customer's library
            //foreach (var item in shoppingCartItems)
            //{
              //  customer.Games.Add(item.ProductName); 
            //}

            // Clear the shopping cart after adding games to the library
            _dbContext.ShoppingCart.RemoveRange(shoppingCartItems);

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            // Return a success response
            return RedirectToAction("ShoppingCart");
        }





    }
}
