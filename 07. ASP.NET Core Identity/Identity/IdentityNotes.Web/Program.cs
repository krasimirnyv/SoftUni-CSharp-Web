namespace IdentityNotes.Web;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

using Data;

using Service.Core;
using Service.Core.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        string connectionString = builder.Configuration.GetConnectionString("DevConnection") ?? 
                                  throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<NoteDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddScoped<INoteService, NoteService>();
        
        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                ConfigureIdentity(options);
            })
            .AddEntityFrameworkStores<NoteDbContext>();
        
        builder.Services.ConfigureApplicationCookie(options =>
        {
            ConfigureCookies(options);
        });
        
        builder.Services.AddControllersWithViews();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
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

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{noteId?}");
        app.MapRazorPages();

        app.Run();
    }
    
    private static void ConfigureIdentity(IdentityOptions options)
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;

        options.User.RequireUniqueEmail = false;
                
        options.Lockout.MaxFailedAccessAttempts = 255;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                
        options.Password.RequireDigit = false;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 1;
    }
    
    private static void ConfigureCookies(CookieAuthenticationOptions options)
    {
        options.LoginPath = "/Identity/Account/Login";
        options.LogoutPath = "/Identity/Account/Logout";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            
        options.SlidingExpiration = true;
            
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.Name = "IdentityAuthCookie";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    }
}
