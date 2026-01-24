namespace BookShelf;

using Data;

using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Load user secrets
        builder.Configuration.AddUserSecrets<Program>();
        
        // Configure database connection
        string? secretConnection = builder.Configuration["ConnectionStrings:MyDevConnection"];
        string? defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
        
        // Prefer user secret connection string over default
        string connectionString = !string.IsNullOrWhiteSpace(secretConnection)
            ? secretConnection
            : defaultConnection
            ?? throw new InvalidOperationException("No valid connection string found!");
        
        // Register the DbContext with the specified connection string
        builder.Services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt
                .UseSqlServer(connectionString);
        });
        
        // Add services to the container.
        builder.Services.AddControllersWithViews();

        WebApplication app = builder.Build();

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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.UseStatusCodePagesWithReExecute("Home/StatusCodeError", "?statusCode={0}");

        app.Run();
    }
}