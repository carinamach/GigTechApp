using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

public partial class GigTechContext : DbContext
{
    public GigTechContext()
    {
    }

    public GigTechContext(DbContextOptions<GigTechContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<ForumPost> ForumPosts { get; set; }

    public virtual DbSet<ForumThread> ForumThreads { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=mssql1.ilait.se;Initial Catalog=dln126504;User ID=udmsln460304;Password=giganajs;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB858D5BCEEF");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();

            entity.HasOne(d => d.Product).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orderDetails_product");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3214EC07BAC593F2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Status).IsFixedLength();

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_ToCustomers");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductID");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__payment__0809337D420211C2");

            entity.Property(e => e.OrderId).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.Payments).HasConstraintName("FK__payment__userID__04E4BC85");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__product__2D10D14A35418549");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Tickets__A4AE64D855B16952");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
