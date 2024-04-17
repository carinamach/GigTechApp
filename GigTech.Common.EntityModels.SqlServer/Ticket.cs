using System.ComponentModel.DataAnnotations;
namespace GigTech.Shared;

public partial class Ticket
{
    [Key]
    public int Id { get; set; }

    public string CustomerId { get; set; } = null!;

    public string? Status { get; set; }

    public string TicketContent { get; set; } = null!;

    public string TicketTitle { get; set; } = null!;

    public DateOnly TicketDate { get; set; }
}
