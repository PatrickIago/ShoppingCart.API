namespace ShoppingCart.Domain.Models;
/// <summary>
/// Classe responsavel pelo endereço
/// </summary>
public class EnderecoEntrega
{
    public int Id { get; private set; }
    public string Logradouro { get; private set; }
    public int Numero { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public string CEP { get; private set; }
    public int? ClienteId { get; set; }

    //Propriedades de navegação
    public virtual Cliente? Cliente { get; private set; }

    public EnderecoEntrega(string logradouro, int numero, string cidade, string estado, string cep, int clienteId)
    {
        Logradouro = logradouro;
        Numero = numero;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
        ClienteId = clienteId;
    }

    public void Atualizar(string logradouro, int numero, string cidade, string estado, string cep, int clienteId)
    {
        Logradouro = logradouro;
        Numero = numero;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
        ClienteId = clienteId;
    }
    public EnderecoEntrega() { }

}
