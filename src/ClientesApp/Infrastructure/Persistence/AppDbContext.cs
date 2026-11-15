using Microsoft.EntityFrameworkCore;
using ClientesApp.Domain.Entities;

namespace ClientesApp.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Ciudad> Ciudades => Set<Ciudad>();
    public DbSet<TipoCliente> TiposCliente => Set<TipoCliente>();
    public DbSet<Genero> Generos => Set<Genero>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}