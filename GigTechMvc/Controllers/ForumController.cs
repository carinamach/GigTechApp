using GigTech.Shared;
using GigTechMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GigTechMvc.Controllers
{
    public class ForumController : Controller
    {
        private readonly GigTechContext _context;

        public IActionResult ForumIndex()
        {
            return View("/Views/Pages/ForumPage.cshtml");
        }

        [HttpPost]
        public IActionResult CreatePost(string postTitle, string postContent)
        {
            if (!string.IsNullOrEmpty(postTitle) && !string.IsNullOrEmpty(postContent))
            {
                var newPost = new ForumPost
                {
                    
                    Title = postTitle,
                    Content = postContent,
                    CreationDate = DateTime.Now, // Assuming you want to set the creation date to the current date and time
                                                 // You might need to set other properties like CustomerId, ThreadId, etc. based on your requirements
                };

                _context.ForumPosts.Add(newPost);
                _context.SaveChanges();

                return RedirectToAction("Detail");
            }

            // If the title or content is empty, return back to the view
            return View();
        }
        //public ForumController(GigTechContext context)
        //{
        //    _context = context;
        //}
        //public IActionResult Index()
        //{
        //    // Retrieve and display all forum threads
        //    var threads = _context.ForumThreads.ToList();
        //    return View(threads);
        //}
        public IActionResult CreateThread()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateThread(ForumThread thread)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        thread.CreationDate = DateTime.Now;
        //        _context.ForumThreads.Add(thread);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(thread);
        //}
        //public IActionResult ViewThread(int id)
        //{
        //    // Retrieve and display a specific thread with its posts
        //    var thread = _context.ForumThreads.FirstOrDefault(t => t.Id == id);
        //    if (thread == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(thread);
        //}
        //[HttpPost]
        //public async Task<IActionResult> ReplyToThread(int threadId, ForumPost post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        post.CreationDate = DateTime.Now;
        //        post.ThreadId = threadId;
        //        _context.ForumPosts.Add(post);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(ViewThread), new { id = threadId });
        //    }
        //    return RedirectToAction(nameof(ViewThread), new { id = threadId });
        //}
    }
}
