namespace ShoppingCart.Application.Dto.EnderecoEntregaDtos;
public class EnderecoEntregaDto
{
    public int Id { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string CEP { get; set; }
    public int ClienteId { get; set; }
}
