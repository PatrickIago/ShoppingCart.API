namespace ShoppingCart.Application.Dto.CarrinhoDtos;
public class ItemCarrinhoDto
{
    public int ProdutoId { get; set; }
    public string NomeProduto { get; set; }
    public decimal PrecoUnitario { get; set; }
    public int Quantidade { get; set; }
    public decimal Subtotal => PrecoUnitario * Quantidade;
}
