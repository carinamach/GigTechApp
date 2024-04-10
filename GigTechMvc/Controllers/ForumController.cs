using GigTech.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigTechMvc.Controllers
{
    public class ForumController : Controller
    {
        private readonly GigTechContext _context;
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
                    CustomerId = 1, // Assuming you have some authentication system to get the current user ID
                    ThreadId = 2, // Assuming you have a way to specify the thread ID
                };

                _context.ForumPosts.Add(forumPost);
                _context.SaveChanges();
            }

            return RedirectToAction("ForumPrivacy");
        }
    }
}
