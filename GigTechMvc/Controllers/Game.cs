namespace GigTechMvc.Controllers
{
    internal class Game
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}