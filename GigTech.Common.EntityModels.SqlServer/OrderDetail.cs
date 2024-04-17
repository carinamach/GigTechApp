using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

[Table("orderDetails")]
[Index("CustomerId", Name = "IX_orderDetails_customer_id")]
[Index("ProductId", Name = "IX_orderDetails_product_id")]
public partial class OrderDetail
{
    [Key]
    public int Id { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("order_date")]
    public DateOnly OrderDate { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string Status { get; set; } = null!;

    [Column("total_amount", TypeName = "decimal(18, 0)")]
    public decimal TotalAmount { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("OrderDetails")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderDetails")]
    public virtual Product Product { get; set; } = null!;

    
}
