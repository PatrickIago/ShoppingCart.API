using ShoppingCart.Application.Dto.EnderecoEntregaDtos;
using ShoppingCart.Domain.Enumerado;
namespace ShoppingCart.Application.Dto.PedidoDtos;
/// <summary>
/// Representa o pedido completo para leitura (o "recibo").
/// </summary>
public class PedidoDto
{
    public int Id { get; set; }
    public StatusPedido StatusPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataPedido { get; set; }
    public EnderecoEntregaDto EnderecoEntrega { get; set; }
    public List<ItemPedidoDto> Itens { get; set; } = new List<ItemPedidoDto>();


}
