using GigTech.Shared;
using GigTechMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GigTechMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GigTechContext _context;


        public HomeController(ILogger<HomeController> logger, GigTechContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new IndexModel();

            // Set data for carousel
            model.Games = _context.Products.Skip(2).Take(5).ToList();
            ViewBag.Games = model.Games;

            // Set data for Top Sellers
            ViewBag.TopSeller = _context.Products.Skip(2).FirstOrDefault(); 
            ViewBag.TopSellerList = _context.Products.Skip(4).Take(5).ToList(); 

            // Set data for Upcoming Games
            ViewBag.UpcomingGames = _context.Products.Skip(7).Take(2).ToList();

            // Set data for Deals
            ViewBag.DealGames = _context.Products.Skip(3).Take(4).ToList();

            return View(model);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}