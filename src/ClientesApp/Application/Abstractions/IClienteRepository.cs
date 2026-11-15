using ClientesApp.Domain.Entities;

namespace ClientesApp.Application.Abstractions;

// ISP: lo especifico de Cliente va en su propia interfaz, sin engordar IRepository<T>.
public interface IClienteRepository : IRepository<Cliente>
{
    Task<IReadOnlyList<Cliente>> GetAllWithRelationsAsync(CancellationToken ct = default);
    Task<bool> ExistsByCedulaAsync(string cedula, int excludeId, CancellationToken ct = default);
    Task<bool> ExistsByEmailAsync(string email, int excludeId, CancellationToken ct = default);
}