﻿using GigTech.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GigTechMvc.Models;//ErrorViewModel
using System.Diagnostics; //Activity


namespace GigTechMvc.Controllers
{
    
    public class ProductController : Controller
    {
        public readonly GigTechContext db = new ();
        public ProductController(GigTechContext injectedContext)
        {
            db = injectedContext;
        }

        public IActionResult Product()
        {
            ProductViewModel model = new
                (
                    
                    Products: db.Products.ToList()
                );
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
