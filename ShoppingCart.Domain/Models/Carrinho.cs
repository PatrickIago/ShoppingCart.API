namespace ShoppingCart.Domain.Models;
/// <summary>
/// Representa o carrinho de compras de um cliente.
/// </summary>
public class Carrinho
{
    public int Id { get; private set; }
    public int ClienteId { get; private set; }
    public virtual Cliente Cliente { get; private set; }

    private readonly List<ItemCarrinho> _itens = new();
    public IReadOnlyCollection<ItemCarrinho> Itens => _itens.AsReadOnly();

    // Construtor para o EF Core
    private Carrinho() { }

    public Carrinho(int clienteId)
    {
        ClienteId = clienteId;
    }

    public void AdicionarItem(Produto produto, int quantidade)
    {
        var itemExistente = _itens
            .FirstOrDefault(i => i.ProdutoId == produto.Id);

        if (itemExistente != null)
        {
            itemExistente.AdicionarQuantidade(quantidade);
        }
        else
        {
            _itens.Add(new ItemCarrinho(this.Id, produto.Id, quantidade));
        }
    }

    public void RemoverItem(int produtoId)
    {
        var itemParaRemover = _itens.FirstOrDefault(i => i.ProdutoId == produtoId);

        if (itemParaRemover != null)
        {
            _itens.Remove(itemParaRemover);
        }
    }

    public void AlterarQuantidadeItem(int produtoId, int novaQuantidade)
    {
        var item = _itens.FirstOrDefault(i => i.ProdutoId == produtoId);
        if (item == null) return;

        if (novaQuantidade <= 0)
        {
            RemoverItem(produtoId);
        }
        else
        {
            item.DefinirQuantidade(novaQuantidade);
        }
    }

    public void Limpar()
    {
        _itens.Clear();
    }

    public decimal CalcularValorTotal()
    {
        return _itens.Sum(item => item.CalcularSubtotal());
    }
}
