using GigTech.Shared;

namespace GigTechMvc.Models 
{   
    public class ForumPostViewModel
    {
        public List<ForumPost> Posts { get; set; }
        public Dictionary<int, List<ForumThread>> PostReplies { get; set; }
    }

}