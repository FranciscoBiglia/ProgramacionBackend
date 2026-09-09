using System.ComponentModel.DataAnnotations;

namespace ProyectoPedido.Models;

public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }
    public string? Nombres { get; set; }

    public ICollection<Producto>? productos { get; set; }
}

