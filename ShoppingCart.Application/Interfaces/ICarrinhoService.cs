using ShoppingCart.Application.Dto.CarrinhoDtos;
namespace ShoppingCart.Application.Interfaces;
/// <summary>
/// Define as operações de negócio para o carrinho de compras.
/// </summary>
public interface ICarrinhoService
{
    /// <summary>
    /// Obtém o carrinho pelo ID de um cliente
    /// </summary>
    Task<CarrinhoDto> GetCarrinhoByClienteIdAsync(int clienteId);

    /// <summary>
    /// Adiciona um item ao carrinho do utilizador.
    /// </summary>
    Task<CarrinhoDto> AdicionarItemAsync(int clienteId, AdicionarItemDto itemDto);

    /// <summary>
    /// Atualiza a quantidade de um item no carrinho.
    /// </summary>
    Task<CarrinhoDto> AtualizarItemAsync(int clienteId, int produtoId, AtualizarItemDto itemDto);

    /// <summary>
    /// Remove um item do carrinho.
    /// </summary>
    Task<CarrinhoDto> RemoverItemAsync(int clienteId, int produtoId);

    /// <summary>
    /// Remove todos os itens do carrinho (esvazia).
    /// </summary>
    Task LimparCarrinhoAsync(int clienteId);
}
