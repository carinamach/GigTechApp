using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GigTech.Shared;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace GigTechMvc.Controllers
{
    public class TicketController : Controller
    {
        private readonly GigTechContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly string _spPg = "~/Views/Pages/Ticket/";
        public TicketController(GigTechContext context, IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<string> RetrieveUserId()
        {
            var _currentUser = await _userManager.GetUserAsync(User);
            if (_currentUser == null) { throw new Exception("User not found."); }
            return _currentUser.Id.ToString();
        }

        // GET: Ticket
        [Authorize]
        public async Task<IActionResult> Index()
        {
            await RetrieveUserId();
            return View(_spPg + "Index.cshtml", await _context.Tickets.ToListAsync());
        }

        // GET: Ticket/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Ticket/Create
        public IActionResult Create()
        {
            string userId = RetrieveUserId().Result;
            ViewBag.UserId = userId;
            if (userId == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(_spPg + "Create.cshtml");
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Status,TicketContent,TicketTitle,TicketDate")] Ticket ticket)
        {
            // Retrieve the current user's ID
            string userId = RetrieveUserId().Result;
            ViewBag.UserId = userId;
            // Set the CustomerId property of the ticket


            // Check if the CustomerId is set correctly
            Console.WriteLine($"CustomerId: {ticket.CustomerId}");
            Console.WriteLine(ModelState.IsValid);

            ticket.CustomerId = userId;
            var ticketId = ticket.CustomerId;
            if (ModelState.IsValid)
            {
                try
                {
                    ticket.CustomerId = userId;
                    // Add the ticket to the context
                    _context.Tickets.Add(ticket);

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Redirect to the Index action upon successful creation
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"An error occurred while saving changes: {ex.Message}");

                    // Return the view with the ticket to display error messages
                    return View(_spPg + "Create.cshtml", ticket);
                }
            }
            return View(_spPg + "Create.cshtml", ticket);
        }

        // GET: Ticket/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,CustomerId,Status,TicketContent,TicketTitle,TicketDate")] Ticket ticket)
        {
            if (id != ticket.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Ticket/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(string id)
        {
            return _context.Tickets.Any(e => e.CustomerId == id);
        }
    }
}
