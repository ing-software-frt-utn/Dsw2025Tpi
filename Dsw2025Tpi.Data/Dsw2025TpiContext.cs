using Dsw2025Tpi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2025Tpi.Data;

public class Dsw2025TpiContext : DbContext
{

public Dsw2025TpiContext(DbContextOptions<Dsw2025TpiContext> options) : base(options)
    {
       
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var dbProduct = modelBuilder.Entity<Product>().ToTable("Products");
         
        dbProduct.Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(50);
        dbProduct.Property(p => p.InternalCode)
            .IsRequired()
            .HasMaxLength(50);
        dbProduct.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
        dbProduct.Property(p => p.Description)
            .HasMaxLength(300);
        dbProduct.Property(p => p.CurrentUnitPrice)
            .HasPrecision(15, 2)
            .IsRequired();
        dbProduct.Property(p => p.StockCuantity)
            .HasMaxLength(10);

        var dbCustomer = modelBuilder.Entity<Customer>().ToTable("Customers");
        dbCustomer.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();
        dbCustomer.Property(c => c.Email)
            .HasMaxLength(100)
            .IsRequired();
        dbCustomer.Property(c => c.PhoneNumber)
            .HasMaxLength(10)
            .IsRequired();

        var dbOrder = modelBuilder.Entity<Order>().ToTable("Orders");
        dbOrder.Property(o => o.ShippingAddress)
            .HasMaxLength(200)
            .IsRequired();
        dbOrder.Property(o => o.BillingAddress)
            .HasMaxLength(200)
            .IsRequired();
        dbOrder.Property(o => o.TotalAmount)
            .HasMaxLength(15)
            .HasPrecision(15, 2);

        var dbOrderItem = modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
        dbOrderItem.Property(oi => oi.Name)
            .HasMaxLength(50)
            .IsRequired();
        dbOrderItem.Property(oi => oi.Description)
            .HasMaxLength(300);
        dbOrderItem.Property(oi => oi.UnitPrice)
            .HasPrecision(15, 2)
            .IsRequired();
        dbOrderItem.Property(oi => oi.Quantity)
            .IsRequired();


    }
    public DbSet<Product> Products { get; set; }

}
