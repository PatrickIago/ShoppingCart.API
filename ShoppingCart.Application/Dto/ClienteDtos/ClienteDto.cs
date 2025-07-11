using ShoppingCart.Application.Dto.EnderecoEntregaDtos;
namespace ShoppingCart.Application.Dto.ClienteDtos;
public class ClienteDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public ICollection<EnderecoEntregaDto> enderecos { get; set; }
}
