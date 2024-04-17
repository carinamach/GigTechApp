using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared
{
    [Table("ShoppingCart")]
    public partial class ShoppingCart
    {
        [Key] // Markera CustomerId som primärnyckel
        [Column("customer_id")]
        [StringLength(10)]
        public string CustomerId { get; set; }

        [Column("product_id")]
        [StringLength(10)]
        public string ProductId { get; set; }

        [Column("product_name")]
        [StringLength(10)]
        public string ProductName { get; set; } = null!;

        [Column("product_price")]
        [StringLength(10)]
        public decimal ProductPrice { get; set; }
    }
}
