using Microsoft.EntityFrameworkCore;
using SPILSalesOrder.Infrastructure.Data;
using SPILSalesOrder.Application.Interfaces;
using SPILSalesOrder.Application.Services;
using SPILSalesOrder.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ============= CORS (MUST be here) =============
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Repos
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Services
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ============= ENABLE CORS HERE =============
app.UseCors("AllowAll");

app.MapControllers();

// Seed some sample data at startup if DB is empty (development convenience)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        db.Database.Migrate();

        if (!db.Clients.Any())
        {
            db.Clients.AddRange(
                new SPILSalesOrder.Domain.Entities.Client { Name = "Acme Corporation", Address1 = "123 Industry Rd", Address2 = "Suite 10", Address3 = "Building A", Suburb = "Northbay", City = "Metropolis", State = "CA", PostCode = "90001" },
                new SPILSalesOrder.Domain.Entities.Client { Name = "Beta Traders", Address1 = "45 Market St", Address2 = "", Address3 = "", Suburb = "Central", City = "Gotham", State = "NY", PostCode = "10001" },
                new SPILSalesOrder.Domain.Entities.Client { Name = "Gamma Supplies", Address1 = "9 Commerce Ave", Address2 = "Floor 2", Address3 = "", Suburb = "Harbor", City = "Coast City", State = "FL", PostCode = "33010" }
            );
            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        // swallow errors during seeding to avoid blocking development. Inspect logs if needed.
        var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
        logger?.LogWarning(ex, "Database seeding failed");
    }
}

app.Run();
