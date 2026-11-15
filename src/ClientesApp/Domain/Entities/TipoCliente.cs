using ClientesApp.Domain.Common;

namespace ClientesApp.Domain.Entities;

public class TipoCliente : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal DescuentoPorcentaje { get; set; }

    public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}