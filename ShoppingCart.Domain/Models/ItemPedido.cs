namespace ShoppingCart.Domain.Models;
public class ItemPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }

    // Propriedades de navegação
    public virtual Produto Produto { get; private set; }
    public virtual Pedido Pedido { get; private set; }

    public ItemPedido(int pedidoId, int produtoId, int quantidade, decimal precoUnitario)
    {
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
    }

    public ItemPedido() { }

}
