using ShoppingCart.Domain.Enumerado;
namespace ShoppingCart.Domain.Models;
public class Pedido
{
    public int Id { get; private set; }
    public decimal ValorTotal { get; private set; }
    public StatusPedido StatusPedido { get; private set; }
    public DateTime DataPedido { get; private set; }
    public int ClienteId { get; private set; }
    public virtual Cliente Cliente { get; private set; }
    public int EnderecoEntregaId { get; private set; }
    public virtual EnderecoEntrega EnderecoEntrega { get; private set; }
    private readonly List<ItemPedido> _itens = new();
    public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();

    private Pedido() { }

    public Pedido(Cliente cliente, EnderecoEntrega endereco, Carrinho carrinho)
    {
        ClienteId = cliente.Id;
        EnderecoEntregaId = endereco.Id;
        DataPedido = DateTime.Now;
        StatusPedido = StatusPedido.Criado;

        foreach (var itemCarrinho in carrinho.Itens)
        {
            var itemPedido = new ItemPedido(this.Id, itemCarrinho.ProdutoId, itemCarrinho.Quantidade, itemCarrinho.Produto.Preco);
            _itens.Add(itemPedido);
        }
        CalcularValorTotal();
    }


    /// <summary>
    /// Marca o pedido como Pago, se o status atual for Criado.
    /// </summary>
    public void MarcarComoPago()
    {
        if (StatusPedido != StatusPedido.Criado)
        {
            throw new InvalidOperationException("Apenas pedidos com status 'Criado' podem ser marcados como pagos.");
        }
        StatusPedido = StatusPedido.Pago;
    }

    /// <summary>
    /// Marca o pedido como Enviado, se o status atual for Pago.
    /// </summary>
    public void MarcarComoEnviado()
    {
        if (StatusPedido != StatusPedido.Pago)
        {
            throw new InvalidOperationException("Apenas pedidos com status 'Pago' podem ser enviados.");
        }
        StatusPedido = StatusPedido.Enviado;
    }

    /// <summary>
    /// Cancela o pedido, se ele ainda não tiver sido enviado.
    /// </summary>
    public void Cancelar()
    {
        if (StatusPedido == StatusPedido.Enviado)
        {
            throw new InvalidOperationException("Não é possível cancelar um pedido que já foi enviado.");
        }
        StatusPedido = StatusPedido.Cancelado;
    }

    private void CalcularValorTotal()
    {
        ValorTotal = _itens.Sum(i => i.PrecoUnitario * i.Quantidade);
    }
}