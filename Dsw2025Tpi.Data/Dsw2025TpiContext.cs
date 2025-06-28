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

            eb.Property(p => p._internalCode)
            .HasMaxLength(50)
            .IsRequired();


            eb.Property(p => p._sku)
            .HasMaxLength(50)
            .IsRequired();


            eb.Property(p => p._name)
            .HasMaxLength(50)
            .IsRequired();



            eb.Property(p => p._description)
            .HasMaxLength(100);


            eb.Property(p => p._currentUnitPrice)
            .HasPrecision(15, 2);

        


        });
        modelBuilder.Entity<Customer>(eb => {
            eb.ToTable("Customers");
            eb.HasKey(p => p.Id);

            eb.Property(c => c._name)
               .HasMaxLength(100)
               .IsRequired();

            eb.Property(c => c._email)
                .HasMaxLength(255)
                .IsRequired();

            eb.Property(c => c._phoneNumber)
                .HasMaxLength(20);

            // Índice único para email
            eb.HasIndex(c => c._email)
                .IsUnique();

        });
        modelBuilder.Entity<Order>(eb => {
            eb.ToTable("Orders");
            eb.HasKey(p => p.Id);

            eb.HasOne<Customer>()// relacion muchos a uno
            .WithMany()
            .HasForeignKey(o => o._customerId)
            .OnDelete(DeleteBehavior.Cascade);

            eb.Property(o => o._shippingAddress)
               .HasMaxLength(500)
               .IsRequired();

            eb.Property(o => o._billingAddress)
                .HasMaxLength(500)
                .IsRequired();

            eb.Property(o => o._notes)
                .HasMaxLength(1000); 

            eb.Property(o => o._totalAmount)
                .HasPrecision(18, 2); 

            eb.Property(o => o._status)
                .HasConversion<string>() // Guarda el enum como string
                .HasMaxLength(50);



        });
        modelBuilder.Entity<OrderItem>(eb => {
            eb.ToTable("OrderItems");
            eb.HasKey(p => p.Id);

            eb.HasOne<Order>()
            .WithMany()
            .HasForeignKey(o => o._orderId);

            eb.HasOne<Product>()
            .WithMany()
            .HasForeignKey(o => o._productId);

            eb.Property(oi => oi._unitPrice)
                .HasPrecision(18, 2);

            eb.Property(oi => oi._subtotal)
                .HasPrecision(18, 2);

        });


    }

}
