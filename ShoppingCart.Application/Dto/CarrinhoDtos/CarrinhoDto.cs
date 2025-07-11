namespace ShoppingCart.Application.Dto.CarrinhoDtos;
public class CarrinhoDto
{
    public int Id { get; set; }
    public List<ItemCarrinhoDto> Itens { get; set; } = new List<ItemCarrinhoDto>();
    public decimal ValorTotal { get; set; }
}
