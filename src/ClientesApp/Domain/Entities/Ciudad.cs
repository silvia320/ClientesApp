using ClientesApp.Domain.Common;

namespace ClientesApp.Domain.Entities;

public class Ciudad : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;

    public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}