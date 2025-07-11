namespace ShoppingCart.Application.Dto.PedidoDtos;
/// <summary>
/// Representa um único item dentro de um pedido para leitura.
/// </summary>
public class ItemPedidoDto
{
    public int ProdutoId { get; set; }
    public string NomeProduto { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}
