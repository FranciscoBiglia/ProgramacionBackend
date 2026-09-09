using System.ComponentModel.DataAnnotations;

namespace ProyectoPedido.Models;

public class Pedido
{
    [Key]
    public int PedidoId { get; set; }
    public Estado Estado { get; set; }
    public decimal Total { get; set; }
    public DateOnly Fecha { get; set; }


    public ICollection<DetallePedido>? DetallePedidos { get; set; }
}

public enum Estado
{
    Pendiente,
    Enviado,
    Entregado
}