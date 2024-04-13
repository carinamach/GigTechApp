using GigTech.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            string currentUserId = _userManager.GetUserAsync(User).ToString();
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
    }
}
