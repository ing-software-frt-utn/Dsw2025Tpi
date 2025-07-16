using Dsw2025Tpi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2025Tpi.Data;

public class Dsw2025TpiContext: DbContext
{
    public Dsw2025TpiContext(DbContextOptions<Dsw2025TpiContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(eb => {

            eb.ToTable("Products");

            // eb.HasIndex(p => p._sku)//para que el indice de _sku sea unico
            //.IsUnique();
            eb.HasKey(p => p.Id);

            eb.Property(p => p.internalCode)
            .HasMaxLength(50)
            .IsRequired();


            eb.Property(p => p.sku)
            .HasMaxLength(50)
            .IsRequired();


            eb.Property(p => p.name)
            .HasMaxLength(50)
            .IsRequired();



            eb.Property(p => p.description)
            .HasMaxLength(100);


            eb.Property(p => p.currentUnitPrice)
            .HasPrecision(15, 2);

        


        });
        modelBuilder.Entity<Customer>(eb => {
            eb.ToTable("Customers");
            eb.HasKey(p => p.Id);

            eb.Property(c => c.name)
               .HasMaxLength(100)
               .IsRequired();

            eb.Property(c => c.email)
                .HasMaxLength(255)
                .IsRequired();

            eb.Property(c => c.phoneNumber)
                .HasMaxLength(20);

            // Índice único para email
            eb.HasIndex(c => c.email)
                .IsUnique();

        });
        modelBuilder.Entity<Order>(eb => {
            eb.ToTable("Orders");
            eb.HasKey(p => p.Id);

            eb.HasOne<Customer>()// relacion muchos a uno
            .WithMany()
            .HasForeignKey(o => o.customerId)
            .OnDelete(DeleteBehavior.Cascade);

            eb.Property(o => o.shippingAddress)
               .HasMaxLength(500)
               .IsRequired();

            eb.Property(o => o.billingAddress)
                .HasMaxLength(500)
                .IsRequired();

            eb.Property(o => o.notes)
                .HasMaxLength(1000);

            eb.Ignore(o => o.totalAmount); 

            eb.Property(o => o.status)
                .HasConversion<string>() // Guarda el enum como string
                .HasMaxLength(50);



        });
        modelBuilder.Entity<OrderItem>(eb => {
            eb.ToTable("OrderItems");
            eb.HasKey(p => p.Id);

            eb.HasOne<Order>()
            .WithMany()
            .HasForeignKey(o => o.orderId);

            eb.HasOne<Product>()
            .WithMany()
            .HasForeignKey(o => o.productId);

            eb.Property(oi => oi.unitPrice)
                .HasPrecision(18, 2);

            eb.Ignore(oi => oi.subtotal); // Ignorar subtotal, se calcula en tiempo de ejecución

        });


    }

}
