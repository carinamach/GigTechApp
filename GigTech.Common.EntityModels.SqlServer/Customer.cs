using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

[Table("customers")]
[Index("ProductId", Name = "IX_customers_product_id")]
public partial class Customer
{
    [Key]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("first_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("phone_number")]
    [StringLength(15)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Column("passwordHASH")]
    [StringLength(256)]
    [Unicode(false)]
    public string PasswordHash { get; set; } = null!;

    [Column("username")]
    [StringLength(16)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("profileImage")]
    [Unicode(false)]
    public string ProfileImage { get; set; } = null!;

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("vMoney")]
    public int? VMoney { get; set; }

    [Column("games")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Games { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [InverseProperty("User")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("ProductId")]
    [InverseProperty("Customers")]
    public virtual Product Product { get; set; } = null!;
}
