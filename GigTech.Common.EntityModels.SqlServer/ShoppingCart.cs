﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

[Keyless]
[Table("ShoppingCart")]
public partial class ShoppingCart
{
    [Column("customer_id")]
    [StringLength(10)]
    public string CustomerId { get; set; } = null!;

    [Column("product_id")]
    [StringLength(10)]
    public string ProductId { get; set; } = null!;

    [Column("product_name")]
    [StringLength(10)]
    public string ProductName { get; set; } = null!;

    [Column("product_price")]
    [StringLength(10)]
    public int ProductPrice { get; set; } 
}
