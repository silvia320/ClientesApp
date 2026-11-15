namespace ClientesApp.Application.Dtos;

// DTO "tonto": solo transporta datos. Las reglas viven en Validators/ClienteValidator.cs (SRP).
public class ClienteDto
{
    public int Id { get; set; }
    public string Cedula { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public string? Direccion { get; set; }
    public bool Activo { get; set; } = true;
    public int CiudadId { get; set; }
    public int TipoClienteId { get; set; }
    public int GeneroId { get; set; }

    // Solo lectura, para el listado (Edad es calculada)
    public int Edad { get; set; }
    public string? CiudadNombre { get; set; }
    public string? TipoClienteNombre { get; set; }
    public string? GeneroNombre { get; set; }
}