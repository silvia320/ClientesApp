using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientesApp.Domain.Entities;

namespace ClientesApp.Infrastructure.Persistence.Configurations;

public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> b)
    {
        b.ToTable("ciudades");
        b.HasKey(x => x.Id);
        b.Property(x => x.Nombre).IsRequired().HasMaxLength(80);
        b.Property(x => x.Provincia).IsRequired().HasMaxLength(80);

        // Datos iniciales (van dentro de la migracion). Fecha fija en UTC.
        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        b.HasData(
            new Ciudad { Id = 1, Nombre = "Quito", Provincia = "Pichincha", CreatedAtUtc = seedDate },
            new Ciudad { Id = 2, Nombre = "Cayambe", Provincia = "Pichincha", CreatedAtUtc = seedDate },
            new Ciudad { Id = 3, Nombre = "Guayaquil", Provincia = "Guayas", CreatedAtUtc = seedDate },
            new Ciudad { Id = 4, Nombre = "Cuenca", Provincia = "Azuay", CreatedAtUtc = seedDate },
            new Ciudad { Id = 5, Nombre = "Ambato", Provincia = "Tungurahua", CreatedAtUtc = seedDate });
    }
}