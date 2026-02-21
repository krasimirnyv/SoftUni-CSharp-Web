using GameZone.Data;
using Microsoft.EntityFrameworkCore;

namespace GameZone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddUserSecrets<Program>();
            
            string? secretConnectionString = builder.Configuration["ConnectionStrings:DevConnection"];
            string? defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            
            string connectionString = !string.IsNullOrWhiteSpace(secretConnectionString)
                ? secretConnectionString
                : defaultConnectionString
                ?? throw new InvalidOperationException("Connection string is not found.");

            // DbContext is registered as Scoped Service
            // In Controller context -> Every new HTTP Request = new instance of DbContext
            builder.Services.AddDbContext<GameDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
