using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

public partial class ForumThread
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public int? ForumPostId { get; set; }

    public int? UserId { get; set; }
}
