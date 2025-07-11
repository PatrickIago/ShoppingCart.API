namespace ShoppingCart.Domain.Models;
/// <summary>
/// Classe responsavel pelos itens no carrinho
/// </summary>
public class ItemCarrinho
{
    public int Id { get; private set; }
    public int CarrinhoId { get; private set; }
    public int ProdutoId { get; private set; }
    public int Quantidade { get; private set; }

    //Propriedades de navegação
    public virtual Produto Produto { get; set; }
    public virtual Carrinho Carrinho { get; set; }

    public ItemCarrinho() { }

    public ItemCarrinho(int carrinhoId, int produtoId, int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser positiva.", nameof(quantidade));

        CarrinhoId = carrinhoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
    }
    public void AdicionarQuantidade(int quantidadeAdicional)
    {
        if (quantidadeAdicional <= 0) return;
        Quantidade += quantidadeAdicional;
    }

    public void DefinirQuantidade(int novaQuantidade)
    {
        if (novaQuantidade <= 0)
            throw new ArgumentException("A quantidade deve ser positiva.", nameof(novaQuantidade));
        Quantidade = novaQuantidade;
    }

    public decimal CalcularSubtotal()
    {
        if (Produto == null)
            throw new InvalidOperationException("O produto não foi carregado para calcular o subtotal.");
        return Produto.Preco * Quantidade;
    }
}

