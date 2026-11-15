using ClientesApp.Application.Dtos;

namespace ClientesApp.Application.Abstractions;

// Listas para los desplegables del formulario.
public interface ILookupService
{
    Task<IReadOnlyList<LookupItemDto>> GetCiudadesAsync(CancellationToken ct = default);
    Task<IReadOnlyList<LookupItemDto>> GetTiposClienteAsync(CancellationToken ct = default);
    Task<IReadOnlyList<LookupItemDto>> GetGenerosAsync(CancellationToken ct = default);
}