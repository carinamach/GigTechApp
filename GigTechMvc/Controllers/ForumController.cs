using Microsoft.AspNetCore.Mvc;

namespace GigTechMvc.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult ForumIndex()
        {
            return View("/Views/Pages/ForumPage.cshtml");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
