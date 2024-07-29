using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace SuperMarket_Antojitos.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleDetail> SaleDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customer
        builder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.Property(e => e.DNI)
                .IsRequired()
                .HasMaxLength(15);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PhoneNumber)
                .IsRequired();

            entity.Property(e => e.Email)
                .IsRequired();
        });

        // Product
        builder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.Property(e => e.ProductCode)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.UnitPrice)
                .IsRequired();

            entity.Property(e => e.UnitsInStock)
                .IsRequired();
        });

        // Sale
        builder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId);

            entity.Property(e => e.SaleDate)
                .IsRequired();

            entity.HasOne(e => e.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // SaleDetail
        builder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(e => e.SaleDetailId);

            entity.Property(e => e.Quantity)
                .IsRequired();

            entity.Property(e => e.TotalPrice)
                .IsRequired();

            entity.HasOne(e => e.Sale)
                .WithMany(s => s.SaleDetails)
                .HasForeignKey(e => e.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Product)
                .WithMany(p => p.SaleDetails)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}