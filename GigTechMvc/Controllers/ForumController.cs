﻿using GigTech.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
            var posts = _context.ForumPosts                             
                                .OrderByDescending(post => post.CreationDate)
                                .ToList();

            return View("/Views/Pages/ForumPage.cshtml", posts);
        }
    
        public IActionResult FilterByCategory(int? categoryNumber)
        {
            List<ForumPost> posts;
            if (categoryNumber.HasValue)
            {
                posts = _context.ForumPosts
                                .Where(post => post.ThreadId == categoryNumber)
                                .OrderByDescending(post => post.CreationDate)                              
                                .ToList();
            }
            else
            {
                posts = _context.ForumPosts
                                .OrderByDescending(post => post.CreationDate)                              
                                .ToList();
            }
            return View("/Views/Pages/ForumPage.cshtml", posts);
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
                return RedirectToAction("ForumIndex");

            }
            return RedirectToAction("ForumIndex");
        }
        [HttpPost]
        public IActionResult EditPost(int postId,string newTitle, string newContent)
        {
            var post = _context.ForumPosts.FirstOrDefault(p => p.Id == postId);

            if (post == null)
            {
                return NotFound();
            }
            else
            {
                if (post.Title != newTitle || post.Content!= newContent)
                {
                    post.Title = newTitle;
                    post.Content = newContent;
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("ForumIndex");
        }

        [HttpPost]
        public IActionResult DeletePost(int postId)
        {
            var post = _context.ForumPosts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                return NotFound();
            }
            _context.ForumPosts.Remove(post);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "An error occurred while deleting the post.");
            }
            return RedirectToAction("ForumIndex");
        }
    }
}
