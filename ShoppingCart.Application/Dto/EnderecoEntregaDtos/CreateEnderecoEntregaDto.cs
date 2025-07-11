namespace ShoppingCart.Application.Dto.EnderecoEntregaDtos;
public class CreateEnderecoEntregaDto
{
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string CEP { get; set; }
    public int ClienteId { get; set; }

    public CreateEnderecoEntregaDto(string logradouro, int numero, string cidade, string estado, string cep, int clienteId)
    {
        Logradouro = logradouro;
        Numero = numero;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
        ClienteId = clienteId;
    }
}
