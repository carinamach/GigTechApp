using GigTech.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigTechMvc.Controllers
{
    public class ForumController : Controller
    {
        private readonly GigTechContext _context;

        public ForumController(GigTechContext injectedContext)
        {
            _context = injectedContext;
        }
        public IActionResult ForumIndex()
        {
            return View("/Views/Pages/ForumPage.cshtml");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveText(string title, string content)
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(content))
            {
                var forumPost = new ForumPost()
                {
                    Title = title,
                    Content = content,
                    CreationDate = DateTime.Now,
                    ThreadId =1,
                    CustomerId = 1, // Assuming you have some authentication system to get the current user ID
                };

                _context.ForumPosts.Add(forumPost);
                _context.SaveChanges();
            }

            return View("/Views/Pages/SupportPage.cshtml");
        }
    }
}
