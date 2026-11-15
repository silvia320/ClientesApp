using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientesApp.Domain.Entities;

namespace ClientesApp.Infrastructure.Persistence.Configurations;

public class GeneroConfiguration : IEntityTypeConfiguration<Genero>
{
    public void Configure(EntityTypeBuilder<Genero> b)
    {
        b.ToTable("generos");
        b.HasKey(x => x.Id);
        b.Property(x => x.Nombre).IsRequired().HasMaxLength(40);

        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        b.HasData(
            new Genero { Id = 1, Nombre = "Masculino", CreatedAtUtc = seedDate },
            new Genero { Id = 2, Nombre = "Femenino", CreatedAtUtc = seedDate },
            new Genero { Id = 3, Nombre = "Otro", CreatedAtUtc = seedDate });
    }
}