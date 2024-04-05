using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GigTech.Shared;
using System.Security.Cryptography;

namespace GigTech.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly GigTechContext _context;

        public ShoppingCartModel(GigTechContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Hämta alla produkter i kundvagnen och skicka till vyn
            var shoppingCart = _context.ShoppingCarts.ToList();
            return Page();
        }

        public IActionResult OnPostPurchase()
        {
            // Hämta spelen från ShoppingCart
            var shoppingCart = _context.ShoppingCarts.ToList();

            if (!shoppingCart.Any())
            {
                return new JsonResult(new { success = false, message = "Shopping cart is empty." });
            }

            // Flytta spelen från ShoppingCart till Customers
            foreach (var product in shoppingCart)
            {
                _context.Customers.Add(new Customer
                {
                    Games = product.ProductName,
                    //ProductPrice = product.ProductPrice
                });
                _context.ShoppingCarts.Remove(product);
            }

            _context.SaveChanges();

            return new JsonResult(new { success = true });
        }

        public IActionResult OnPostRemoveProduct(int productId)
        {
            var productToRemove = _context.ShoppingCarts.FirstOrDefault(p => p.CustomerId == productId);
            if (productToRemove != null)
            {
                _context.ShoppingCarts.Remove(productToRemove);
                _context.SaveChanges();
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false });
            
        }
    }
}
