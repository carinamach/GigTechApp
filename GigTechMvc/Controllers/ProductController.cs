using GigTech.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GigTechMvc.Models;//ErrorViewModel
using System.Diagnostics;
using System.Linq; //Activity


namespace GigTechMvc.Controllers
{

    public class ProductController : Controller
    {
        public readonly GigTechContext db = new();
        public ProductController(GigTechContext injectedContext)
        {
            db = injectedContext;
        }

        public IActionResult Product()
        {

            return View("/Views/Pages/ProductPage.cshtml");
        }
        public async Task<IActionResult> ProductDetailPage(int? id)
        {

            Product? model = await db.Products.SingleOrDefaultAsync(p => p.ProductId == id);

            if (model is null)
            {
                return NotFound($"ProductId {id} not found.");
            }
            return View("/Views/Pages/ProductDetailPage.cshtml", model);
        }
        public IActionResult AddToCart(int? id)
        {
            ShoppingCart shoppingcart = new ShoppingCart();


            Product? product = db.Products.FirstOrDefault(p => p.ProductId == id);

            shoppingcart.ProductId = product.ProductId.ToString();
            shoppingcart.ProductName = product.Title;
            shoppingcart.ProductPrice = product.Price;


            db.ShoppingCart.Add(shoppingcart);

            db.SaveChanges();
            return View("/Views/Pages/ProductPage.cshtml");
        }
        [HttpPost]
        public IActionResult reviewcomment(string username, string title, string review, int gameid)
        {
            List<Gamereview> gamelist;
            gamelist = db.Gamereviews.ToList();
            int a = gamelist.Count;
            var createreview = new Gamereview()
            {
                CommentId = a + 1,
                Title = title,
                Username = username,
                Review = review,
                GameId = gameid // Associate review with game
            };

            db.Gamereviews.Add(createreview);
            db.SaveChanges();

            return View("/Views/Pages/ProductPage.cshtml");
        }
        public IActionResult FilterByGenre(string genre) {
            List<Product> games;
            if (genre != "")
            {
                games = db.Products
                                .Where(game => game.Category == genre)
                                .ToList();
            }
            else
            {
                games = db.Products
                                .ToList();
            }
            return View("/Views/Pages/ProductPage.cshtml", games);
        }

    }
}

