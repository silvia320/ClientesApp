using Microsoft.EntityFrameworkCore;
using ClientesApp.Application.Abstractions;
using ClientesApp.Domain.Entities;

namespace ClientesApp.Infrastructure.Persistence;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(AppDbContext db) : base(db) { }

    public async Task<IReadOnlyList<Cliente>> GetAllWithRelationsAsync(CancellationToken ct = default)
        => await Db.Set<Cliente>()
            .AsNoTracking()
            .Include(c => c.Ciudad)
            .Include(c => c.TipoCliente)
            .Include(c => c.Genero)
            .OrderBy(c => c.Id)
            .ToListAsync(ct);

    public Task<bool> ExistsByCedulaAsync(string cedula, int excludeId, CancellationToken ct = default)
    {
        var value = cedula.Trim();
        return Db.Set<Cliente>().AnyAsync(c => c.Cedula == value && c.Id != excludeId, ct);
    }

    public Task<bool> ExistsByEmailAsync(string email, int excludeId, CancellationToken ct = default)
    {
        var value = email.Trim().ToLowerInvariant();
        return Db.Set<Cliente>().AnyAsync(c => c.Email == value && c.Id != excludeId, ct);
    }
}