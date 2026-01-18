namespace GarageApp.Data;

using Microsoft.EntityFrameworkCore;

using Models;

public class GarageDbContext : DbContext
{
    /* This is the proper constructor that will be used by ASP.NET Core while using DI */
    /* When using DbContext through DI container, we will be using DbContextOptions<T> to configure the DbContext*/
    public GarageDbContext(DbContextOptions<GarageDbContext> dbContextOptions) 
        : base(dbContextOptions)
    {
    }
    
    public virtual DbSet<Garage> Garages { get; set; } = null!;
    
    public virtual DbSet<Car> Cars { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(GarageDbContext).Assembly);
    }
}