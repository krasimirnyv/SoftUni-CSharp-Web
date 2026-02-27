namespace Homies.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    
    using Models;

    public class HomiesDbContext : IdentityDbContext
    {
        public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Event> Events { get; set; } = null!;
        
        public virtual DbSet<EventType> EventTypes { get; set; } = null!;
        
        public virtual DbSet<EventParticipant> EventsParticipants { get; set; } = null!;
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        { 
            this.ChangeTracker
                .Entries<Event>()
                .Where(e => e.State == EntityState.Added)
                .ToList()
                .ForEach(ee =>
                {
                    if (ee.Entity.CreatedOn == default)
                    {
                        ee.Entity.CreatedOn = DateTime.UtcNow;
                    }
                });
            
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
                    
            builder.ApplyConfigurationsFromAssembly(typeof(HomiesDbContext).Assembly);
        }
    }
}