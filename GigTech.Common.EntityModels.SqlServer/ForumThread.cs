using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

public partial class ForumThread
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string? Category { get; set; }

    [InverseProperty("Thread")]
    public virtual ICollection<ForumPost> ForumPosts { get; set; } = new List<ForumPost>();
}
