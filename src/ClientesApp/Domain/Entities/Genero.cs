using ClientesApp.Domain.Common;

namespace ClientesApp.Domain.Entities;

// Tabla aparte (catalogo): el genero del cliente viene de aqui y no esta escrito en el codigo.
public class Genero : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}