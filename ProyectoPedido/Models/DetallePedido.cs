using System.ComponentModel.DataAnnotations;

namespace ProyectoPedido.Models;

public class DetallePedido
{
    [Key]
    public int DetallePedidoId { get; set; }
    public decimal PrecioUnitario { get; set; }
    public int Cantidad { get; set; }


    public int ProductoId { get; set; }
	public virtual Producto? Producto { get; set; }

    public int PedidoId { get; set; }
	public virtual Pedido? Pedidos { get; set; }

}

