using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity;
namespace GigTech.Shared;

public partial class Ticket
{
    public int Id { get; }

    [Key]
    public string CustomerId { get; set; } = null!;

    public string? Status { get; set; }

    public string TicketContent { get; set; } = null!;

    public string TicketTitle { get; set; } = null!;

    public DateOnly TicketDate { get; set; }
}
