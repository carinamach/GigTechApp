using GigTech.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigTechMvc.Controllers
{
    
    public class ProductController : Controller
    {
        public readonly GigTechContext db = new ();

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
    }
}
