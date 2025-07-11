namespace ShoppingCart.Application.Dto.LoginDTOs;
/// <summary>
/// Requisitos para criar um usuario
/// </summary>
public class RegistroDto
{
    /// <summary>
    /// Entre como seu nome
    /// </summary>
    public string Nome { get; set; }
    /// <summary>
    /// Entre como seu Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// E selecione uma senha 
    /// </summary>
    public string Senha { get; set; }
}
