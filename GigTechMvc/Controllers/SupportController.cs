using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GigTech.Common.DataContext;
using GigTech.Common.Views.Pages;
using GigTech.Shared;
using GigTechMvc.Models;
using System.Diagnostics;
using System.Dynamic;

namespace ProjectGigTech.Controllers
{
	public class SupportController : Controller
	{
        string _spPg = "~/Views/Pages/SupportPages/";
        public GigTechContext _context = new GigTechContext();
		private readonly IConfiguration _config;
		private readonly UserManager<IdentityUser> _userManager; //Create a manager for the IdentityUser class.
		public SupportController(IConfiguration config, UserManager<IdentityUser> userManager)
		{
			_config = config;
			_userManager = userManager; // I have no clue what this does, it's necessary though.
		}

        public IActionResult Index()
        {
            return View("~/Views/Pages/SupportPage.cshtml", new SupportPageModel());
        }
		public async Task<IActionResult> RetrieveUserId()
		{

            var currentUser = await _userManager.GetUserAsync(User); // sets currentUser to be the value of the current User.

			if (currentUser == null){return Unauthorized();} // If Unauthorized/not logged in, return.
            else
			{
                string userId = currentUser.Id; // Self explanatory.
                return Ok(userId); // SUCCESS state: Returns the UserId.
			}
		}

		[Authorize]
        public IActionResult CreateTicket()
		{

			var _userId = RetrieveUserId().Id.ToString(); // Retrieves the string from function above. Ensures it's a String

			var viewModel = new Ticket // Create a new Ticket model in which the UserId and Date is passed.
			{
				CustomerId = _userManager.GetUserId(User)!,
				TicketDate =  DateOnly.FromDateTime(DateTime.Now)
            };
            return View(_spPg + "CreateTicket.cshtml", viewModel); // show the View and pass in the data above.
			// In the View. Retrieve above 
		}

		[Authorize]
		public IActionResult AccountInfo() 
		{
			return View("~/Views/Pages/UserPage.cshtml");
		}

		[Authorize]
		public IActionResult Tickets()
		{
			var viewModel = new Ticket
			{

			};
			return View(_spPg + "ViewTickets.cshtml", viewModel);
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}

}
