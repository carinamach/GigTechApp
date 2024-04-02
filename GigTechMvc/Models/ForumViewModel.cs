using GigTech.Shared;

namespace GigTechMvc.Models 
{
    public class ForumIndexViewModel
    {
        public List<ForumThread> ForumThreads { get; set; }
    }

    public class CreateThreadViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class ViewThreadViewModel
    {
        public ForumThread Thread { get; set; }
        public List<ForumPost> Posts { get; set; }
        public ForumPost NewPost { get; set; } 
    }
}
