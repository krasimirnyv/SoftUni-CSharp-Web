using GarageApp.Data;
using Microsoft.EntityFrameworkCore;

namespace GarageApp;

public class Program
{
    public static void Main(string[] args)
    {
        /* Registration of user services in DI container */
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        string? connectionString = builder.Configuration.GetConnectionString("DevConnection");
        
        builder.Services.AddDbContext<GarageDbContext>(opt =>
        {
            opt
                .UseSqlServer(connectionString);
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        /* Application configuration */
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