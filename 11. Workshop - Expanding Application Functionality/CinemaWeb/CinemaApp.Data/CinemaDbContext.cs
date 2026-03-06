namespace CinemaApp.Data;

using Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class CinemaDbContext : IdentityDbContext<IdentityUser>
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> Movies { get; set; } = null!;

    public virtual DbSet<UserMovie> UsersMovies { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(CinemaDbContext).Assembly);
    }
}