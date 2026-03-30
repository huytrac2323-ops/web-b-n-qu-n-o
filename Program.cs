using Microsoft.EntityFrameworkCore;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add authentication (cookie) and authorization
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.LoginPath = "/Account/Login";
});

// Add EF Core SQL Server
builder.Services.AddDbContext<Demo.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Server=(localdb)\\mssqllocaldb;Database=DemoDb;Trusted_Connection=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Try applying migrations and seeding, but don't crash the app on mismatch
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var db = services.GetRequiredService<Demo.Data.ApplicationDbContext>();
    try
    {
        db.Database.Migrate();

        // Seed default admin user if none exists
        if (!db.Users.Any())
        {
            db.Users.Add(new Demo.Models.User
            {
                Username = "admin",
                PasswordHash = Demo.Models.User.HashPassword("123"),
                Roles = "Admin"
            });
            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Database migration or seeding failed. Continuing without applying migrations.");
    }

    // Log Products table schema
    try
    {
        DbConnection conn = db.Database.GetDbConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Products';";
        using var reader = cmd.ExecuteReader();
        logger.LogInformation("Products table columns:");
        while (reader.Read())
        {
            var col = reader.IsDBNull(0) ? "" : reader.GetString(0);
            var dt = reader.IsDBNull(1) ? "" : reader.GetString(1);
            var nn = reader.IsDBNull(2) ? "" : reader.GetString(2);
            logger.LogInformation("{Column} | {Type} | {Nullable}", col, dt, nn);
        }
        conn.Close();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to query Products columns.");
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
