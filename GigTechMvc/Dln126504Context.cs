using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GigTech.Shared;

public partial class Dln126504Context : DbContext
{
    public Dln126504Context()
    {
    }

    public Dln126504Context(DbContextOptions<Dln126504Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=mssql1.ilait.se; Initial Catalog=dln126504;User ID=udmsln460304;Password=giganajs;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.Property(e => e.CostumerId).IsFixedLength();
            entity.Property(e => e.ProductId).IsFixedLength();
            entity.Property(e => e.ProductName).IsFixedLength();
            entity.Property(e => e.ProductPrice).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
