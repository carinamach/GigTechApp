using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace GigTech.Shared
{
    public class IndexModel() : PageModel
    {
        public List<Product> Games { get; set; }

        public void OnGet(){}
    }
}