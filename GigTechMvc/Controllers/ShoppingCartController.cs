using GigTech.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigTechMvc.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly GigTechContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartController(GigTechContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<string> RetrieveUserId()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return currentUser?.Id;
        }

        [Authorize]
        public IActionResult ShoppingCart()
        {
            var shoppingCartItems = _dbContext.ShoppingCart.ToList();
            ViewBag.ShoppingCart = shoppingCartItems;
            return View("/Views/Pages/ShoppingCart.cshtml");
        }

        [HttpPost]
        public IActionResult RemoveCustomerCart(int customerId)
        {
            try
            {
                var cartItems = _dbContext.ShoppingCart.Where(item => item.CustomerId == customerId.ToString()).ToList();
                if (cartItems.Any())
                {
                    _dbContext.ShoppingCart.RemoveRange(cartItems);
                    _dbContext.SaveChanges();
                }
                return RedirectToAction("Index", "Home"); // Redirect to a relevant page after removal
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while removing the cart items: " + ex.Message);
            }
        }








        
        













        [HttpPost]
        public async Task<IActionResult> Pay()
        {
            try
            {
                // Retrieve the current user's ID
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return RedirectToAction("Index", "Home"); // Or any other action you prefer
                }
                // Retrieve the customer from the database
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == currentUser.Email);
                if (customer == null)
                {
                    return NotFound("User not found.");
                }

                // Retrieve shopping cart items for the current user
                var shoppingCartItems = _dbContext.ShoppingCart
                .Where(item => item.CustomerId == customer.CustomerId.ToString())
                .ToList();

                decimal totalPrice = shoppingCartItems.Sum(item => item.ProductPrice);
                int totalPriceInt = (int)totalPrice;

                if (customer.VMoney < totalPriceInt)
                {
                    return BadRequest("Insufficient funds.");
                }

                // Deduct the total price from the customer's vMoney
                customer.VMoney -= totalPriceInt;
                var currentGames = customer.Games.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                var productIds = shoppingCartItems.Select(item => item.ProductId).ToList();
                currentGames.AddRange(productIds);
                customer.Games = string.Join(',', currentGames);
                _dbContext.ShoppingCart.RemoveRange(shoppingCartItems);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Handle exception if necessary
                return StatusCode(500, "An error occurred during payment: " + ex.Message);
            }
        }
    }
}
