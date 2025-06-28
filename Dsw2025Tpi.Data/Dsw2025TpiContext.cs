using Microsoft.EntityFrameworkCore;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Data;

public class Dsw2025TpiContext: DbContext
{
    public Dsw2025TpiContext(DbContextOptions<Dsw2025TpiContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>(eb =>
        {
            eb.ToTable("Customers");
            eb.Property(p => p.Email)
            .HasMaxLength(320)
            .IsRequired();
            eb.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
            eb.Property(p => p.PhoneNumber)
            .HasMaxLength(10);
        });
        modelBuilder.Entity<Product>(eb =>
        {
            eb.ToTable("Products");
            eb.Property(p => p.Sku)
            .HasMaxLength(10)
            .IsRequired();
            eb.Property(p => p.InternalCode)
            .HasMaxLength(10)
            .IsRequired();
            eb.Property(p => p.Name)
            .HasMaxLength(30)
            .IsRequired();
            eb.Property(p => p.Description)
            .HasMaxLength(100);
            eb.Property(p => p.CurrentUnitPrice)
            .HasPrecision(10, 2)
            .IsRequired();
            eb.Property(p => p.StockQuantity)
            .IsRequired();
        });
    }
}
