namespace BookVerse.Web;

using Data;

using Services.Core;
using Services.Core.Contracts;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        string connectionString = builder.Configuration.GetConnectionString("DevConnection")
                                  ?? throw new InvalidOperationException("Connection string 'DevConnection' not found.");

        builder.Services.AddDbContext<BookVerseDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        builder.Services.AddDefaultIdentity<IdentityUser>(ConfigureDefaultIdentityOptions)
            .AddEntityFrameworkStores<BookVerseDbContext>();

        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        
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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();
    }

    private static void ConfigureDefaultIdentityOptions(IdentityOptions options)
    {
        options.SignIn.RequireConfirmedAccount = false;
                
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    }
}