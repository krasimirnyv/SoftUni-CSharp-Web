using CinemaApp.Data.Repository.Contracts;

namespace CinemaApp.Data.Repository;

public abstract class BaseRepository : IDisposable
{
    private bool isDisposed = false;
    private readonly CinemaDbContext dbContext;

    protected BaseRepository(CinemaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    protected CinemaDbContext Context => dbContext;
    
    protected async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
        
        isDisposed = true;
    }
}