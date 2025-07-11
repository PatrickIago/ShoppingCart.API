using ShoppingCart.Application.Dto.PedidoDtos;
namespace ShoppingCart.Application.Interfaces;
public interface IPedidoService
{
    /// <summary>
    /// Cria um novo pedido a partir do carrinho de um cliente (processo de Checkout).
    /// </summary>
    /// <param name="clienteId">O ID do cliente que está a fazer a compra.</param>
    /// <param name="createPedidoDto">Os dados para a criação do pedido, como o endereço de entrega.</param>
    /// <returns>O DTO do pedido recém-criado.</returns>
    Task<PedidoDto> CreatePedidoAsync(int clienteId, CreatePedidoDto createPedidoDto);

    /// <summary>
    /// Obtém o histórico de pedidos de um cliente.
    /// </summary>
    /// <param name="clienteId">O ID do cliente.</param>
    /// <returns>Uma lista com os DTOs dos pedidos do cliente.</returns>
    Task<List<PedidoDto>> GetPedidosByClienteIdAsync(int clienteId);

    /// <summary>
    /// Obtém um pedido específico de um cliente.
    /// </summary>
    /// <param name="clienteId">O ID do cliente dono do pedido.</param>
    /// <param name="pedidoId">O ID do pedido a ser procurado.</param>
    /// <returns>O DTO do pedido encontrado.</returns>
    Task<PedidoDto> GetPedidoByIdAsync(int clienteId, int pedidoId);

    // --- NOVOS MÉTODOS PARA ATUALIZAR O STATUS ---

    /// <summary>
    /// Altera o status de um pedido para 'Pago'.
    /// </summary>
    /// <param name="pedidoId">O ID do pedido a ser atualizado.</param>
    /// <returns>O DTO do pedido atualizado.</returns>
    Task<PedidoDto> MarcarPedidoComoPagoAsync(int pedidoId);

    /// <summary>
    /// Altera o status de um pedido para 'Enviado'.
    /// </summary>
    /// <param name="pedidoId">O ID do pedido a ser atualizado.</param>
    /// <returns>O DTO do pedido atualizado.</returns>
    Task<PedidoDto> MarcarPedidoComoEnviadoAsync(int pedidoId);

    /// <summary>
    /// Altera o status de um pedido para 'Cancelado'.
    /// </summary>
    /// <param name="pedidoId">O ID do pedido a ser cancelado.</param>
    /// <returns>O DTO do pedido atualizado.</returns>
    Task<PedidoDto> CancelarPedidoAsync(int pedidoId);
}
