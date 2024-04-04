using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GigTech.Common.Views.Pages;
using GigTech.Shared;
using GigTechMvc.Models;
using System.Diagnostics;

namespace GigTechMvc.Controllers
{
	public class SupportController : Controller
	{
        string _spPg = "~/Views/Pages/SupportPages/";
        public GigTechContext _context = new GigTechContext();
		public SupportController(){}
		
        public IActionResult Index()
        {
            return View("~/Views/Pages/SupportPage.cshtml", new SupportPageModel());
        }

		[Authorize]
		public IActionResult CreateTicket()
		{
			return View("~/Views/Pages/Ticket/Create.cshtml");
		}

		public IActionResult ViewTickets()
		{
			return View("~/Views/Pages/Ticket/Index.cshtml");
		}

		[Authorize]
		public IActionResult AccountInfo() 
		{
			return View("~/Views/Pages/UserPage.cshtml");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}

}
