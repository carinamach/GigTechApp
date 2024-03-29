using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

[Index("Id", Name = "UQ__Tickets__3214EC06973D6D9F", IsUnique = true)]
[Index("CustomerId", Name = "UQ__Tickets__A4AE64D93668228A", IsUnique = true)]
public partial class Ticket
{
    public int Id { get; set; }

    [Key]
    public string CustomerId { get; set; } = null!;

    public string? Status { get; set; }

    public string TicketContent { get; set; } = null!;

    public string TicketTitle { get; set; } = null!;

    public DateOnly TicketDate { get; set; }
}
