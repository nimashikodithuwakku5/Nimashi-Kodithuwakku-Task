using Microsoft.EntityFrameworkCore;
using SPILSalesOrder.Domain.Entities;

namespace SPILSalesOrder.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<OrderHeader> OrderHeaders { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // CLIENT
        modelBuilder.Entity<Client>(b =>
        {
            b.ToTable("Client");
            b.HasKey(x => x.ClientId);

            b.Property(x => x.Name).HasMaxLength(100).IsRequired();
            b.Property(x => x.Address1).HasMaxLength(200);
            b.Property(x => x.Address2).HasMaxLength(200);
            b.Property(x => x.City).HasMaxLength(100);
            b.Property(x => x.Address3).HasMaxLength(200);
            b.Property(x => x.Suburb).HasMaxLength(100);
            b.Property(x => x.State).HasMaxLength(50);
            b.Property(x => x.PostCode).HasMaxLength(20);
            
            // Seed some sample clients for development/testing
            b.HasData(
                new Client { ClientId = 1, Name = "Acme Corporation", Address1 = "123 Industry Rd", Address2 = "Suite 10", Address3 = "Building A", Suburb = "Northbay", City = "Metropolis", State = "CA", PostCode = "90001" },
                new Client { ClientId = 2, Name = "Beta Traders", Address1 = "45 Market St", Address2 = "", Address3 = "", Suburb = "Central", City = "Gotham", State = "NY", PostCode = "10001" },
                new Client { ClientId = 3, Name = "Gamma Supplies", Address1 = "9 Commerce Ave", Address2 = "Floor 2", Address3 = "", Suburb = "Harbor", City = "Coast City", State = "FL", PostCode = "33010" }
            );
        });

        // ITEM
        modelBuilder.Entity<Item>(b =>
        {
            b.ToTable("Item");
            b.HasKey(x => x.ItemId);

            b.Property(x => x.ItemCode).HasMaxLength(50).IsRequired();
            b.Property(x => x.Description).HasMaxLength(200).IsRequired();
            b.Property(x => x.Price).HasColumnType("decimal(18,2)");
        });

        // ORDER HEADER
        modelBuilder.Entity<OrderHeader>(b =>
        {
            b.ToTable("OrderHeader");
            b.HasKey(x => x.OrderId);

            b.Property(x => x.OrderDate).IsRequired();

            b.Property(x => x.Address1).HasMaxLength(200);
            b.Property(x => x.Address2).HasMaxLength(200);
            b.Property(x => x.Address3).HasMaxLength(200);
            b.Property(x => x.Suburb).HasMaxLength(100);
            b.Property(x => x.State).HasMaxLength(50);
            b.Property(x => x.PostalCode).HasMaxLength(20);

            b.Property(x => x.InvoiceNo).HasMaxLength(100);
            b.Property(x => x.InvoiceDate);
            b.Property(x => x.ReferenceNo).HasMaxLength(100);
            b.Property(x => x.Note).HasMaxLength(1000);

            b.HasMany(x => x.Details)
             .WithOne(d => d.OrderHeader!)
             .HasForeignKey(d => d.OrderId);
        });

        // ORDER DETAIL
        modelBuilder.Entity<OrderDetail>(b =>
        {
            b.ToTable("OrderDetail");
            b.HasKey(x => x.OrderDetailId);

            b.Property(x => x.Description).HasMaxLength(200);
            b.Property(x => x.Note).HasMaxLength(300);

            b.Property(x => x.Quantity).HasColumnType("decimal(18,2)");
            b.Property(x => x.Price).HasColumnType("decimal(18,2)");
            b.Property(x => x.TaxRate).HasColumnType("decimal(18,2)");
            b.Property(x => x.ExclAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.TaxAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.InclAmount).HasColumnType("decimal(18,2)");
        });
    }
}
