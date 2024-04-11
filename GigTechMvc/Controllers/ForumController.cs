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
        public IActionResult SaveText(string title, string content, int threadId)
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(content))
            {
                content = content.Replace("\n", "<br />");

                var forumPost = new ForumPost()
                {
                    Title = title,
                    Content = content,
                    CreationDate = DateTime.Now,
                    ThreadId = threadId,
                    CustomerId = 1, // Assuming you have some authentication system to get the current user ID
                };

                _context.ForumPosts.Add(forumPost);
                _context.SaveChanges();
                return View("/Views/Pages/ForumPage.cshtml");

            }
            else return View("/Views/Home/Privacy.cshtml");
        }
    }
}
