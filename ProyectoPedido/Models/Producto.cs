using System.ComponentModel.DataAnnotations;

namespace ProyectoPedido.Models;

public class Producto
{
    [Key]
    public int ProductoId { get; set; }
    public string? Nombres { get; set; }
    public string? Descripcion { get; set; }
    public decimal Costo { get; set; }
    public decimal Venta { get; set; }
    public int stock { get; set; }


    public int CategoriaId { get; set; }
	public virtual Categoria? Categoria { get; set; }


    public ICollection<DetallePedido>? DetallePedidos { get; set; }
}


public class VistaProducto
{
    public int ProductoId { get; set; }
    public string? NombreProducto { get; set; }
    public string? DescripcionProducto { get; set; }
    public decimal CostoProducto { get; set; }
    public decimal VentaProducto { get; set; }
    public int stockProducto { get; set; }
    

    public int CategoriaId { get; set; }
    public string? NombreCategoria { get; set; }

}
