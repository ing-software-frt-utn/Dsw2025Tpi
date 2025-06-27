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

    }

}
