using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Common.EntityModels.SqlServer;

[Index("ThreadId", Name = "IX_ForumPosts_ThreadId")]
public partial class ForumPostW
{
    [Key]
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public int CustomerId { get; set; }

    public int ThreadId { get; set; }

    public int? RepliesCount { get; set; }

    [StringLength(255)]
    public string? Title { get; set; }
}
