namespace ShoppingCart.Application.Dto.ProdutoDTOs;
public class CreateProdutoDto
{
    public string Nome { get; private set; }
    public string DescricaoProduto { get; private set; }
    public decimal Preco { get; private set; }
    public int Estoque { get; private set; }

    public CreateProdutoDto(string nome, string descricaoProduto, decimal preco, int estoque)
    {
        Nome = nome;
        DescricaoProduto = descricaoProduto;
        Preco = preco;
        Estoque = estoque;
    }
}
