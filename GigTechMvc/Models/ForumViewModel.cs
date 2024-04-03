using GigTech.Shared;

namespace GigTechMvc.Models 
{
    public record ForumIndexViewModel
    (   IList<ForumThread> ForumThreads,
        IList<ForumPost> ForumPosts);
    

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
