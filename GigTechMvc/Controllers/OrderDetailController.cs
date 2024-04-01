using GigTech.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GigTechMvc.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly GigTechContext _dbContext;

        public OrderDetailController(GigTechContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Åtgärd för att ta bort en produkt från kundvagnen
        public IActionResult RemoveFromCart(int productId)
        {
            var productToRemove = _dbContext.Products.Find(productId);

            if (productToRemove == null)
            {
                return NotFound();
            }

            // Implementera logik för att ta bort produkten från kundvagnen här

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "ShoppingCart");
        }

        // Åtgärd för att visa orderdetaljer
        public IActionResult OrderDetail()
        {
            return View("/Views/Pages/OrderDetail.cshtml");
        }

        // Åtgärd för att hantera köpet
        [HttpPost]
        public IActionResult Purchase()
        {
            // Implementera logik för att hantera köpet här
            // För nuvarande exempel, låt oss bara anta att köpet lyckas
            var success = true;

            if (success)
            {
                // Om köpet lyckades, lägg till spelet i användarens bibliotek eller utför andra åtgärder
                return Json(new { success = true });
            }
            else
            {
                // Om köpet misslyckades på grund av bristande pengar
                return Json(new { success = false });
            }
        }
    }
}
