
using Dsw2025Tpi.Application.Services;
using Dsw2025Tpi.Data;
using Dsw2025Tpi.Data.Repositories;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Dsw2025Tpi.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();

        builder.Services.AddDbContext<Dsw2025TpiContext>(option =>
        {
            option.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Dsw2025Db;Integrated Security=True");
        });

        builder.Services.AddScoped<IRepository, EfRepository>();

        builder.Services.AddScoped<ProductsManagmentService>();

        builder.Services.AddScoped<OrderManagementService>();

        builder.Services.AddControllers();

        var app = builder.Build();

        //seed clientes
        using (var scope = app.Services.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<Dsw2025TpiContext>();

            if (!ctx.Customers.Any())
            {
                // Ruta al JSON en el directorio de salida
                var dataFolder = Path.Combine(AppContext.BaseDirectory, "Data");
                var filePath = Path.Combine(dataFolder, "customers.json");

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"No encontr� {filePath}");

                var json = File.ReadAllText(filePath);
                var customers = JsonSerializer.Deserialize<List<Customer>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (customers?.Any() == true)
                {
                    ctx.Customers.AddRange(customers);
                    ctx.SaveChanges();
                }
            }
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        app.MapHealthChecks("/healthcheck");

        app.Run();
    }
}
