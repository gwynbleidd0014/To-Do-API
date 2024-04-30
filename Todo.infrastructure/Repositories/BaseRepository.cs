using Microsoft.EntityFrameworkCore;

namespace Todo.infrastructure.Repositories;

public class BaseRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbset;

    public BaseRepository(DbContext context)
    {
        _context = context;
        _dbset = context.Set<T>();
    }

    protected async Task<List<T>> GetAllAsync(CancellationToken token)
    { 
        return await _dbset.ToListAsync(token).ConfigureAwait(false);
    }

    protected async Task<T?> GetAsync(CancellationToken token, params object[] key) 
    {
        return await _dbset.FindAsync(key, token).ConfigureAwait(false);
    }

    protected async Task AddAsync(CancellationToken token, T entity)
    { 
         await _dbset.AddAsync(entity, token).ConfigureAwait(false);
    }

    protected Task Update(T entity)
    { 
        _dbset.Update(entity);
        return Task.CompletedTask;
    }

    protected async Task RemoveAsync(CancellationToken token, params object[] key)
    {
        var entity = await GetAsync(token, key).ConfigureAwait(false);
        if (entity is null)
            return;

        _dbset.Remove(entity);
    }

}
