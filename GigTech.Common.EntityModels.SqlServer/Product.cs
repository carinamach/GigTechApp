using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

[Table("product")]
public partial class Product
{
    [Key]
    [Column("productID")]
    public int ProductId { get; set; }

    [Column("title")]
    [StringLength(100)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Column("category")]
    [StringLength(50)]
    [Unicode(false)]
    public string Category { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("releaseDate")]
    public DateOnly? ReleaseDate { get; set; }

    [Column("small_image")]
    [StringLength(300)]
    [Unicode(false)]
    public string? SmallImage { get; set; }

    [Column("image_one")]
    [StringLength(300)]
    [Unicode(false)]
    public string? ImageOne { get; set; }

    [Column("image_two")]
    [StringLength(300)]
    [Unicode(false)]
    public string? ImageTwo { get; set; }

    [Column("image_three")]
    [StringLength(300)]
    [Unicode(false)]
    public string? ImageThree { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
