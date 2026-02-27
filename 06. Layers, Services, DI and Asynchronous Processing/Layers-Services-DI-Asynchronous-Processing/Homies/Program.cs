namespace Homies
{
    using Data;

    using Service.Core;
    using Service.Core.Interfaces;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = builder.Configuration.GetConnectionString("DevConnection")
                                      ?? throw new InvalidOperationException("Connection string 'DevConnection' not found.");
            /* Register DbContext for DI */
            builder.Services.AddDbContext<HomiesDbContext>(options => 
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            /* Register custom Services in DI */
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IEventUserService, EventUserService>();
                
            /* Configure and register Identity for DI */
            builder.Services.AddDefaultIdentity<IdentityUser>(options => 
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<HomiesDbContext>();

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

            /* Allows us to use default Authorization of Identity */
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}