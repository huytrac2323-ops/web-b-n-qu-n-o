using Microsoft.EntityFrameworkCore;
using Demo.Models;

namespace Demo.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Demo.Models.User> Users { get; set; }
    public DbSet<Agent> Agents { get; set; }
    public DbSet<Demo.Models.CustomerGroup> CustomerGroups { get; set; }
    public DbSet<Demo.Models.Customer> Customers { get; set; }
    public DbSet<Demo.Models.CustomerAddress> CustomerAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Products mapping: existing DB uses ProductID/ProductName, avoid seeding incompatible product columns
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("ProductID").HasColumnType("bigint").ValueGeneratedOnAdd();
            entity.Property(p => p.Name).HasColumnName("ProductName").IsRequired().HasColumnType("nvarchar(max)");
            entity.Property(p => p.ProductCode).HasColumnName("ProductCode").HasColumnType("varchar(max)");
            entity.Property(p => p.CompanyId).HasColumnName("CompanyID").HasColumnType("bigint");
            entity.Property(p => p.CategoryId).HasColumnName("CategoryID").HasColumnType("bigint");
            entity.Property(p => p.ProductTypeId).HasColumnName("ProductTypeID").HasColumnType("bigint");
            entity.Property(p => p.BaseUnitId).HasColumnName("BaseUnitID").HasColumnType("bigint");
            entity.Property(p => p.DefaultTaxRateId).HasColumnName("DefaultTaxRateID").HasColumnType("bigint");
            entity.Property(p => p.IsActive).HasColumnName("IsActive").HasColumnType("bit");
            entity.Property(p => p.IsDeleted).HasColumnName("IsDeleted").HasColumnType("bit");
            entity.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime2");
        });

        // Seed data for Agents
        modelBuilder.Entity<Agent>().HasData(
            new Agent { Id = 1, Name = "John Doe", Company = "Tech Solutions", Status = "Không Hoạt Động", RunDate = new DateTime(2024, 12, 5), TargetAmount = 85, Performance = 15 },
            new Agent { Id = 2, Name = "Sarah Miller", Company = "Innovate Inc", Status = "Hoạt Động", RunDate = new DateTime(2024, 12, 12), TargetAmount = 92, Performance = 64 },
            new Agent { Id = 3, Name = "Interjohn Phelps", Company = "Hardware", Status = "Đang Chờ", RunDate = new DateTime(2024, 12, 12), TargetAmount = 68, Performance = 54 },
            new Agent { Id = 4, Name = "David Clark", Company = "Financial", Status = "Hoạt Động", RunDate = new DateTime(2024, 12, 5), TargetAmount = 95, Performance = 97 }
        );

        // Customer related tables
        modelBuilder.Entity<Demo.Models.CustomerGroup>(entity =>
        {
            entity.ToTable("CustomerGroups");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("CustomerGroupId").HasColumnType("bigint").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired().HasColumnType("nvarchar(200)");
            entity.Property(e => e.Description).HasColumnType("nvarchar(max)");
            entity.Property(e => e.IsActive).HasColumnType("bit");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime2");
        });

        modelBuilder.Entity<Demo.Models.Customer>(entity =>
        {
            entity.ToTable("Customers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("CustomerId").HasColumnType("bigint").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired().HasColumnType("nvarchar(200)");
            entity.Property(e => e.Email).HasColumnType("nvarchar(200)");
            entity.Property(e => e.Phone).HasColumnType("nvarchar(50)");
            entity.Property(e => e.IsActive).HasColumnType("bit");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime2");
            entity.HasOne(e => e.Group).WithMany().HasForeignKey(e => e.CustomerGroupId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Demo.Models.CustomerAddress>(entity =>
        {
            entity.ToTable("CustomerAddresses");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("CustomerAddressId").HasColumnType("bigint").ValueGeneratedOnAdd();
            entity.Property(e => e.AddressLine).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(e => e.City).HasColumnType("nvarchar(200)");
            entity.Property(e => e.State).HasColumnType("nvarchar(200)");
            entity.Property(e => e.PostalCode).HasColumnType("nvarchar(50)");
            entity.Property(e => e.Country).HasColumnType("nvarchar(200)");
            entity.Property(e => e.IsPrimary).HasColumnType("bit");
            entity.HasOne(e => e.Customer).WithMany(c => c.Addresses).HasForeignKey(e => e.CustomerId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
