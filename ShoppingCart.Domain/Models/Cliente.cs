namespace ShoppingCart.Domain.Models;
/// <summary>
/// Classe responsável pelos dados do cliente.
/// </summary>
public class Cliente
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }

    //Propriedades de navegação
    public virtual ICollection<EnderecoEntrega> Enderecos { get; set; } = new List<EnderecoEntrega>();
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public Cliente(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
    private Cliente() { }

    public void Atualizar(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}