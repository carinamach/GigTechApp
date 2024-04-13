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
            var currentUser = await _userManager.GetUserAsync(User);
            var currentUserId = currentUser.Id;
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
            var currentUser = await _userManager.GetUserAsync(User);
            var currentUserId = currentUser.Id;

            // Retrieve shopping cart items for the current user
            var shoppingCartItems = _dbContext.ShoppingCart.Where(item => item.CustomerId == currentUserId).ToList();

            var currentUserIdInt = Convert.ToInt32(currentUserId);
            var user = await _dbContext.Customers.Include(u => u.Games).FirstOrDefaultAsync(u => u.CustomerId == currentUserIdInt);


            // Add games from the shopping cart to the user's games
            if (user != null)
            {
                // Skapa en ny lista för spel
                var newGames = new List<Game>();

                // Loopa igenom varje artikel i shoppingvagnen
                foreach (var item in shoppingCartItems)
                {
                    // Skapa ett nytt Game-objekt och lägg till det i den nya listan
                    newGames.Add(new Game { ProductId = item.ProductId, ProductName = item.ProductName, ProductPrice = item.ProductPrice });
                }

                // Konvertera den nya listan av spel till en sträng
                var gamesAsString = string.Join(",", newGames.Select(g => $"ProductId: {g.ProductId}, ProductName: {g.ProductName}, ProductPrice: {g.ProductPrice}"));

                // Tilldela den nya strängen med spel till användarens Games
                user.Games = gamesAsString;
            }
            return NotFound("User not found.");
        }




    }
}
