using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Dto.PedidoDtos;
using ShoppingCart.Application.Interfaces;
namespace ShoppingCart.API.Controllers;
/// <summary>
/// Controller para gestão administrativa de pedidos.
/// </summary>
[ApiController]
[Route("api/admin/pedidos")]
public class AdminPedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public AdminPedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    /// <summary>
    /// Marca um pedido como 'Pago'.
    /// </summary>
    /// <param name="pedidoId">O ID do pedido a ser atualizado.</param>
    [HttpPatch("{pedidoId}/pagar")]
    public async Task<ActionResult<PedidoDto>> MarcarComoPago(int pedidoId)
    {
        try
        {
            var pedidoAtualizado = await _pedidoService.MarcarPedidoComoPagoAsync(pedidoId);
            return Ok(pedidoAtualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Marca um pedido como 'Enviado'.
    /// </summary>
    /// <param name="pedidoId">O ID do pedido a ser atualizado.</param>
    [HttpPatch("{pedidoId}/enviar")]
    public async Task<ActionResult<PedidoDto>> MarcarComoEnviado(int pedidoId)
    {
        try
        {
            var pedidoAtualizado = await _pedidoService.MarcarPedidoComoEnviadoAsync(pedidoId);
            return Ok(pedidoAtualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Cancela um pedido.
    /// </summary>
    /// <param name="pedidoId">O ID do pedido a ser cancelado.</param>
    [HttpPatch("{pedidoId}/cancelar")]
    public async Task<ActionResult<PedidoDto>> Cancelar(int pedidoId)
    {
        try
        {
            var pedidoAtualizado = await _pedidoService.CancelarPedidoAsync(pedidoId);
            return Ok(pedidoAtualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
