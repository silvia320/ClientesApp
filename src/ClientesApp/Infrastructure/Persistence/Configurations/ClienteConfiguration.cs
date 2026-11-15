using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientesApp.Domain.Entities;

namespace ClientesApp.Infrastructure.Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> b)
    {
        b.ToTable("clientes");
        b.HasKey(x => x.Id);

        b.Property(x => x.Cedula).IsRequired().HasMaxLength(10);
        b.Property(x => x.Nombres).IsRequired().HasMaxLength(80);
        b.Property(x => x.Apellidos).IsRequired().HasMaxLength(80);
        b.Property(x => x.Email).IsRequired().HasMaxLength(120);
        b.Property(x => x.Telefono).IsRequired().HasMaxLength(15);
        b.Property(x => x.Direccion).HasMaxLength(200);
        b.Ignore(x => x.Edad);   // campo calculado: no existe como columna

        b.HasIndex(x => x.Cedula).IsUnique();
        b.HasIndex(x => x.Email).IsUnique();

        b.HasOne(x => x.Ciudad)
            .WithMany(c => c.Clientes)
            .HasForeignKey(x => x.CiudadId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.TipoCliente)
            .WithMany(t => t.Clientes)
            .HasForeignKey(x => x.TipoClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.Genero)
            .WithMany(g => g.Clientes)
            .HasForeignKey(x => x.GeneroId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}