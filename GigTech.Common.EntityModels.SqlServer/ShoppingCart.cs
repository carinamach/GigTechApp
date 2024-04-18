using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

[Table("ShoppingCart")]
public partial class ShoppingCart
{
    [Column("customer_id")]
    [StringLength(10)]
    public string? CustomerId { get; set; }

    [Key]
    [Column("product_id")]
    [StringLength(10)]
    public string ProductId { get; set; } = null!;

    [Column("product_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ProductName { get; set; }

    [Column("product_price", TypeName = "decimal(18, 2)")]
    public decimal? ProductPrice { get; set; }

    [Column("newPrimaryKey")]
    public int? NewPrimaryKey { get; set; }
}
