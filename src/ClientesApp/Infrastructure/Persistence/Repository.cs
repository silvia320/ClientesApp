using Microsoft.EntityFrameworkCore;
using ClientesApp.Application.Abstractions;
using ClientesApp.Domain.Common;

namespace ClientesApp.Infrastructure.Persistence;

// LSP: cualquier Repository<T> puede sustituir a IRepository<T>.
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Db;
    public Repository(AppDbContext db) => Db = db;

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
        => await Db.Set<T>().AsNoTracking().OrderBy(x => x.Id).ToListAsync(ct);

    public Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        => Db.Set<T>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await Db.Set<T>().AddAsync(entity, ct);
        await Db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        Db.Set<T>().Update(entity);
        await Db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        Db.Set<T>().Remove(entity);
        await Db.SaveChangesAsync(ct);
    }
}