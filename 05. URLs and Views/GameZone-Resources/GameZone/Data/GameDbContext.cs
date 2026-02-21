namespace GameZone.Data
{
    using Models;
    
    using Microsoft.EntityFrameworkCore;

    public class GameDbContext(DbContextOptions<GameDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Game> Games { get; set; } = null!;
        
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameDbContext).Assembly);
        }
    }
}
