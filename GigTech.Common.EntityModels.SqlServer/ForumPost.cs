using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

public partial class ForumPost
{
    [Key]
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public int CustomerId { get; set; }

    public int ThreadId { get; set; }

    [StringLength(255)]
    public string? Title { get; set; }

}
