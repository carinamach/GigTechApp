using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

[Table("payment")]
[Index("UserId", Name = "IX_payment_userID")]
public partial class Payment
{
    [Key]
    [Column("orderID")]
    public int OrderId { get; set; }

    [Column("userID")]
    public int? UserId { get; set; }

    [Column("pInformation")]
    [Unicode(false)]
    public string? PInformation { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Payments")]
    public virtual Customer? User { get; set; }
}
