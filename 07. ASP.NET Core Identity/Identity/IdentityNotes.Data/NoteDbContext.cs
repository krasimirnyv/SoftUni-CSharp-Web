namespace IdentityNotes.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Models;

public class NoteDbContext : IdentityDbContext
{
    public NoteDbContext(DbContextOptions<NoteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Note> Notes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(NoteDbContext).Assembly);
    }
}