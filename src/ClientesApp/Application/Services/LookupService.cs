using ClientesApp.Application.Abstractions;
using ClientesApp.Application.Dtos;
using ClientesApp.Domain.Entities;

namespace ClientesApp.Application.Services;

public class LookupService : ILookupService
{
    private readonly IRepository<Ciudad> _ciudades;
    private readonly IRepository<TipoCliente> _tipos;
    private readonly IRepository<Genero> _generos;

    public LookupService(IRepository<Ciudad> ciudades, IRepository<TipoCliente> tipos, IRepository<Genero> generos)
    {
        _ciudades = ciudades;
        _tipos = tipos;
        _generos = generos;
    }

    public async Task<IReadOnlyList<LookupItemDto>> GetCiudadesAsync(CancellationToken ct = default)
        => (await _ciudades.GetAllAsync(ct))
            .OrderBy(c => c.Nombre)
            .Select(c => new LookupItemDto(c.Id, $"{c.Nombre} ({c.Provincia})"))
            .ToList();

    public async Task<IReadOnlyList<LookupItemDto>> GetTiposClienteAsync(CancellationToken ct = default)
        => (await _tipos.GetAllAsync(ct))
            .OrderBy(t => t.Nombre)
            .Select(t => new LookupItemDto(t.Id, t.Nombre))
            .ToList();

    public async Task<IReadOnlyList<LookupItemDto>> GetGenerosAsync(CancellationToken ct = default)
        => (await _generos.GetAllAsync(ct))
            .OrderBy(g => g.Id)
            .Select(g => new LookupItemDto(g.Id, g.Nombre))
            .ToList();
}