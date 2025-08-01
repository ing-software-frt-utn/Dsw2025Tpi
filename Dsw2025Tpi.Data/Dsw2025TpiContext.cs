using Microsoft.EntityFrameworkCore;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Data;

public class Dsw2025TpiContext: DbContext 
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; } 
    public DbSet<OrderItem> OrderItems { get; set; } 
    public DbSet<Customer> Customers { get; set; }

    public Dsw2025TpiContext(DbContextOptions<Dsw2025TpiContext> options)
            : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) //Configuracion FluentApi
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(productBuilder =>
        {
            productBuilder.HasIndex(p => p.Sku)
                .IsUnique();
            productBuilder.Property(p => p.Sku)
                .HasMaxLength(20)
                .IsRequired();
            productBuilder.Property(p => p.InternalCode)
                .HasMaxLength(20)
                .IsRequired();
            productBuilder.Property(p => p.Name)
                .HasMaxLength(60)
                .IsRequired();
            productBuilder.Property(p => p.Description)
                .HasMaxLength(100)
                .IsRequired();
            productBuilder.Property(p => p.CurrentUnitPrice)
                .HasPrecision(15, 2)
                .IsRequired();
            productBuilder.Property(p => p.StockQuantity)
                .IsRequired();      
        });

        modelBuilder.Entity<Order>(orderBuilder =>
        {
            orderBuilder.Property(o => o.ShippingAddress)
                .HasMaxLength(60)
                .IsRequired();
            orderBuilder.Property(o => o.BillingAddress)
                .HasMaxLength(60)
                .IsRequired();
            orderBuilder.Property(o => o.Notes)
                .HasMaxLength(100)
                .IsRequired(false);
            orderBuilder.Property(o => o.TotalAmount)
                .HasPrecision(15, 2)
                .IsRequired();

            orderBuilder.Property(o => o.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            orderBuilder.HasMany(o => o.Items) 
                .WithOne(i => i.Order)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            orderBuilder.HasOne(o => o.Customer) 
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OrderItem>(orderItemBuilder =>
        {
            orderItemBuilder.Property(i => i.Quantity)
                .IsRequired();
            orderItemBuilder.Property(i => i.UnitPrice)
                .HasPrecision(10, 2)
                .IsRequired();
            orderItemBuilder.Property(i => i.SubTotal) 
                .HasPrecision(15, 2)
                .IsRequired();

            orderItemBuilder.HasOne(i => i.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Customer>(customerBuilder =>
        {
            customerBuilder.Property(c => c.EMail)
                .HasMaxLength(100)
                .IsRequired();
            customerBuilder.Property(c => c.Name)
                .HasMaxLength(60)
                .IsRequired();
            customerBuilder.Property(c => c.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();
        });

       

    }

}
