namespace CinemaApp.Web;

using Data;

using Services.Core;
using Services.Core.Contracts;

using Data.Repository;
using Data.Repository.Contracts;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        builder.Configuration.AddUserSecrets<Program>();
        
        string? secretConnection = builder.Configuration["ConnectionStrings:DefaultConnection"];
        string? defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
        
        string connectionString = !string.IsNullOrWhiteSpace(secretConnection)
            ? secretConnection
            : defaultConnection 
              ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        
        builder.Services.AddDbContext<CinemaDbContext>(options =>
        {
            options.UseSqlServer(connectionString, opt =>
            {
                opt.EnableRetryOnFailure(5);
            });
        });
        
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Add Data Protection
        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "DataProtectionKeys")))
            .SetApplicationName("CinemaApp");

        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                ConfigureIdentity(builder.Configuration, options);
            })
            .AddEntityFrameworkStores<CinemaDbContext>();

        // Configure cookie options
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = ".AspNetCore.Identity.Application";
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.LoginPath = "/Identity/Account/Login";
            options.LogoutPath = "/Identity/Account/Logout";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(14);
        });

        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
        
        builder.Services.AddScoped<IMovieService, MovieService>();
        
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        
        // Add cookie policy
        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = _ => false;
            options.MinimumSameSitePolicy = SameSiteMode.Lax;
        });
        
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

        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }
        
        app.UseCookiePolicy();
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
    
    private static void ConfigureIdentity(ConfigurationManager configuration, IdentityOptions options)
    {
        options.SignIn.RequireConfirmedAccount = configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
        options.SignIn.RequireConfirmedEmail = configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
        options.SignIn.RequireConfirmedPhoneNumber = configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

        options.Password.RequireDigit = configuration.GetValue<bool>("Identity:Password:RequireDigit");
        options.Password.RequiredLength = configuration.GetValue<int>("Identity:Password:RequiredLength");
        options.Password.RequiredUniqueChars = configuration.GetValue<int>("Identity:Password:RequiredUniqueChars");
        options.Password.RequireLowercase = configuration.GetValue<bool>("Identity:Password:RequireLowercase");
        options.Password.RequireNonAlphanumeric = configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
        options.Password.RequireUppercase = configuration.GetValue<bool>("Identity:Password:RequireUppercase");
    }
}