namespace ShoppingCart.Application.Dto.ProdutoDTOs;
public class UpdateProdutoDto
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string DescricaoProduto { get; private set; }
    public decimal Preco { get; private set; }
    public int Estoque { get; private set; }

    public UpdateProdutoDto(int id, string nome, string descricaoProduto, decimal preco, int estoque)
    {
        Id = id;
        Nome = nome;
        DescricaoProduto = descricaoProduto;
        Preco = preco;
        Estoque = estoque;
    }
}
