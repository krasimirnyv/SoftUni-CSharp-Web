namespace EventManager.Data;

using Microsoft.EntityFrameworkCore;

using Models;

public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions<EventDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Event> Events { get; set; } = null!;
    
    public virtual DbSet<Category> Categories { get; set; } = null!;
    
    public virtual DbSet<Registration> Registrations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventDbContext).Assembly);
    }
}