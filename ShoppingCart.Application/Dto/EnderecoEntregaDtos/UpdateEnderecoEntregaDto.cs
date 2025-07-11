namespace ShoppingCart.Application.Dto.EnderecoEntregaDtos;
public class UpdateEnderecoEntregaDto
{
    public int Id { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string CEP { get; set; }
    public int ClienteId { get; set; }

    public UpdateEnderecoEntregaDto(int id, string logradouro, int numero, string cidade, string estado, string cEP, int clienteId)
    {
        Id = id;
        Logradouro = logradouro;
        Numero = numero;
        Cidade = cidade;
        Estado = estado;
        CEP = cEP;
        ClienteId = clienteId;
    }
}
