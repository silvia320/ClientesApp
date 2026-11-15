using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientesApp.Domain.Entities;

namespace ClientesApp.Infrastructure.Persistence.Configurations;

public class TipoClienteConfiguration : IEntityTypeConfiguration<TipoCliente>
{
    public void Configure(EntityTypeBuilder<TipoCliente> b)
    {
        b.ToTable("tipos_cliente");
        b.HasKey(x => x.Id);
        b.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
        b.Property(x => x.Descripcion).HasMaxLength(200);
        b.Property(x => x.DescuentoPorcentaje).HasColumnType("numeric(5,2)");

        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        b.HasData(
            new TipoCliente { Id = 1, Nombre = "Regular", Descripcion = "Cliente sin beneficios", DescuentoPorcentaje = 0m, CreatedAtUtc = seedDate },
            new TipoCliente { Id = 2, Nombre = "Premium", Descripcion = "Cliente frecuente", DescuentoPorcentaje = 10m, CreatedAtUtc = seedDate },
            new TipoCliente { Id = 3, Nombre = "Corporativo", Descripcion = "Empresas con convenio", DescuentoPorcentaje = 15m, CreatedAtUtc = seedDate });
    }
}