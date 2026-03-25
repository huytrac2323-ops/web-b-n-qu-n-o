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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for Products
        modelBuilder.Entity<Product>().HasData(
            // Flash Sale Section
            new Product { Id = 1, Name = "Áo Phông Trắng", Price = 199000, Image = "https://via.placeholder.com/200/ffffff/000000?text=Áo+Phông+Trắng", Category = "Flash Sale", Rating = 4.8, Reviews = 24, Description = "Áo phông trắng cơ bản, thoải mái và dễ kết hợp" },
            new Product { Id = 2, Name = "Quần Shorts Jean", Price = 299000, Image = "https://via.placeholder.com/200/0066cc/ffffff?text=Quần+Shorts", Category = "Flash Sale", Rating = 4.9, Reviews = 18, Description = "Quần shorts jean mát mẻ, phù hợp cho mùa hè" },
            new Product { Id = 3, Name = "Áo Khoác Denim", Price = 499000, Image = "https://via.placeholder.com/200/663300/ffffff?text=Áo+Khoác", Category = "Flash Sale", Rating = 4.7, Reviews = 32, Description = "Áo khoác denim bền bỉ và thời trang" },
            new Product { Id = 4, Name = "Áo Crop Top Xanh", Price = 249000, Image = "https://via.placeholder.com/200/0099ff/ffffff?text=Crop+Top", Category = "Flash Sale", Rating = 5.0, Reviews = 15, Description = "Áo crop top xanh nước biển, trẻ trung và gợi cầm" },
            
            // Trend Luxe Section
            new Product { Id = 5, Name = "Áo Vest Đen", Price = 799000, Image = "https://via.placeholder.com/200/333333/ffffff?text=Áo+Vest", Category = "Trend Luxe", Rating = 4.9, Reviews = 45, Description = "Áo vest đen cao cấp, phù hợp cho các sự kiện chính thức" },
            new Product { Id = 6, Name = "Váy Trắng Dự Tiệc", Price = 899000, Image = "https://via.placeholder.com/200/f0f0f0/000000?text=Váy+Trắng", Category = "Trend Luxe", Rating = 5.0, Reviews = 38, Description = "Váy trắng dự tiệc elegant, làm nổi bật vẻ đẹp" },
            new Product { Id = 7, Name = "Áo Sơ Mi Lụa", Price = 699000, Image = "https://via.placeholder.com/200/cccccc/666666?text=Áo+Sơ+Mi", Category = "Trend Luxe", Rating = 4.8, Reviews = 28, Description = "Áo sơ mi lụa mịn, có phần mềm mại và thoáng khí" },
            new Product { Id = 8, Name = "Quần Tây Xám", Price = 599000, Image = "https://via.placeholder.com/200/808080/ffffff?text=Quần+Tây", Category = "Trend Luxe", Rating = 4.7, Reviews = 22, Description = "Quần tây xám đơn giản, dễ kết hợp với nhiều áo" },
            
            // Thời Trang Hot Trend
            new Product { Id = 9, Name = "Áo Len Đen", Price = 349000, Image = "https://via.placeholder.com/200/1a1a1a/ffffff?text=Áo+Len", Category = "Hot Trend", Rating = 4.9, Reviews = 51, Description = "Áo len đen ấm áp, thích hợp cho những ngày lạnh" },
            new Product { Id = 10, Name = "Váy Đen Midi", Price = 459000, Image = "https://via.placeholder.com/200/2d2d2d/ffffff?text=Váy+Midi", Category = "Hot Trend", Rating = 4.8, Reviews = 42, Description = "Váy đen midi kinh điển, phù hợp múi lạnh hàng ngày" },
            new Product { Id = 11, Name = "Áo Trắng Nữ", Price = 249000, Image = "https://via.placeholder.com/200/ffffff/000000?text=Áo+Trắng", Category = "Hot Trend", Rating = 5.0, Reviews = 67, Description = "Áo trắng nữ đơn giản nhưng sang trọng" },
            new Product { Id = 12, Name = "Áo Đỏ Dự Tiệc", Price = 549000, Image = "https://via.placeholder.com/200/cc0000/ffffff?text=Áo+Đỏ", Category = "Hot Trend", Rating = 4.9, Reviews = 38, Description = "Áo đỏ dự tiệc nổi bật, tự tin trong các dịp đặc biệt" },
            
            // Thời Trang Nam
            new Product { Id = 13, Name = "Quần Jeans Nam", Price = 379000, Image = "https://via.placeholder.com/200/003d7a/ffffff?text=Quần+Jeans", Category = "Thời Trang Nam", Rating = 4.8, Reviews = 55, Description = "Quần jeans nam bền bỉ, dễ mix and match" },
            new Product { Id = 14, Name = "Áo Sơ Mi Nam", Price = 299000, Image = "https://via.placeholder.com/200/ffffff/000000?text=Áo+Sơ+Mi", Category = "Thời Trang Nam", Rating = 4.7, Reviews = 33, Description = "Áo sơ mi nam chất lượng, thích hợp cho công sở" },
            new Product { Id = 15, Name = "Áo Khoác Nam", Price = 599000, Image = "https://via.placeholder.com/200/4d4d4d/ffffff?text=Áo+Khoác", Category = "Thời Trang Nam", Rating = 4.9, Reviews = 48, Description = "Áo khoác nam ấm áp và thời trang" },
            new Product { Id = 16, Name = "Quần Tây Nam", Price = 449000, Image = "https://via.placeholder.com/200/333333/ffffff?text=Quần+Tây", Category = "Thời Trang Nam", Rating = 4.8, Reviews = 29, Description = "Quần tây nam sang trọng, phù hợp cho các dịp quan trọng" }
        );

        // Seed data for Agents
        modelBuilder.Entity<Agent>().HasData(
            new Agent { Id = 1, Name = "John Doe", Company = "Tech Solutions", Status = "Không Hoạt Động", RunDate = new DateTime(2024, 12, 5), TargetAmount = 85, Performance = 15 },
            new Agent { Id = 2, Name = "Sarah Miller", Company = "Innovate Inc", Status = "Hoạt Động", RunDate = new DateTime(2024, 12, 12), TargetAmount = 92, Performance = 64 },
            new Agent { Id = 3, Name = "Interjohn Phelps", Company = "Hardware", Status = "Đang Chờ", RunDate = new DateTime(2024, 12, 12), TargetAmount = 68, Performance = 54 },
            new Agent { Id = 4, Name = "David Clark", Company = "Financial", Status = "Hoạt Động", RunDate = new DateTime(2024, 12, 5), TargetAmount = 95, Performance = 97 }
        );
    }
}
