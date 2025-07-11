namespace ShoppingCart.Domain.Models;
/// <summary>
/// Classe responsável pelos dados do produto.
/// </summary>
public class Produto
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string DescricaoProduto { get; private set; }
    public decimal Preco { get; private set; }
    public int Estoque { get; private set; }

    public Produto(string nome, string descricaoProduto, decimal preco, int estoque)
    {
        Nome = nome;
        DescricaoProduto = descricaoProduto;
        Preco = preco;
        Estoque = estoque;
    }

    // Construtor para o EF Core
    private Produto() { }

    public void Atualizar(string nome, string descricaoProduto, decimal preco, int estoque)
    {
        Nome = nome;
        DescricaoProduto = descricaoProduto;
        Preco = preco;
        Estoque = estoque;
    }

    /// Remove uma quantidade específica do estoque, validando se há o suficiente.
    public void RemoverDoEstoque(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentException("A quantidade a ser removida deve ser positiva.", nameof(quantidade));
        }

        if (Estoque < quantidade)
        {
            throw new InvalidOperationException("Estoque insuficiente.");
        }

        Estoque -= quantidade;
    }
}