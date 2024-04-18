using GigTech.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
                // Hämta den aktuella användarens ID
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return RedirectToAction("Index", "Home"); // Eller någon annan åtgärd du föredrar
                }

                // Hämta kunden från databasen
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == currentUser.Email);
                if (customer == null)
                {
                    return NotFound("User not found.");
                }

                // Hämta varukorgsobjekt för den aktuella användaren
                var shoppingCartItems = _dbContext.ShoppingCart
                    .Where(item => item.CustomerId == customer.CustomerId.ToString())
                    .ToList();

                // Beräkna totalpriset
                decimal totalPrice = (decimal)shoppingCartItems.Sum(item => item.ProductPrice);

                // Kontrollera om kunden har tillräckligt med pengar
                if (customer.VMoney < totalPrice)
                {
                    return BadRequest("Insufficient funds.");
                }

                // Dra av det totala priset från kundens vMoney
                customer.VMoney -= (int)totalPrice;

                // Ta bort varukorgsobjekt
                _dbContext.ShoppingCart.RemoveRange(shoppingCartItems);

                // Spara ändringar i databasen
                await _dbContext.SaveChangesAsync();
                //return RedirectToAction("Index", "Home"); // Redirect to a relevant page after removal
                return RedirectToAction("Receipt", "ShoppingCart");


            }
            catch (Exception ex)
            {
                // Hantera undantag om det behövs
                return StatusCode(500, "An error occurred during payment: " + ex.Message);
            }
        }







        public IActionResult Receipt()
        {
            var orderDetails = _dbContext.OrderDetails.ToList();
            return View("/Views/Shared/Receipt.cshtml", orderDetails);
        }




    }
}
