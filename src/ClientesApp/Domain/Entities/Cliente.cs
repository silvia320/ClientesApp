using ClientesApp.Domain.Common;

namespace ClientesApp.Domain.Entities;

public class Cliente : BaseEntity
{
    public string Cedula { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public string? Direccion { get; set; }
    public bool Activo { get; set; } = true;

    // Campo CALCULADO: se obtiene de FechaNacimiento, no se guarda en la BD.
    public int Edad
    {
        get
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);
            var edad = hoy.Year - FechaNacimiento.Year;
            if (FechaNacimiento > hoy.AddYears(-edad)) edad--;
            return edad;
        }
    }

    // Relaciones (N clientes -> 1 ciudad, N clientes -> 1 tipo de cliente)
    public int CiudadId { get; set; }
    public Ciudad? Ciudad { get; set; }

    public int TipoClienteId { get; set; }
    public TipoCliente? TipoCliente { get; set; }

    public int GeneroId { get; set; }
    public Genero? Genero { get; set; }
}