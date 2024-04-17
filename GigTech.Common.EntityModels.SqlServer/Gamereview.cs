using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

[Table("Gamereview")]
public partial class Gamereview
{
    [Key]
    [Column("commentID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommentId { get; set; }

    [Column("title")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Title { get; set; }

    [Column("username")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Username { get; set; }

    [Column("review", TypeName = "text")]
    public string? Review { get; set; }

    [Column("gameID")]
    public int? GameId { get; set; }
}
