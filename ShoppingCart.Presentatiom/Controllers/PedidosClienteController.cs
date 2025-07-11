using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Dto.PedidoDtos;
using ShoppingCart.Application.Interfaces;
namespace ShoppingCart.API.Controllers;
/// <summary>
/// Controller para gerir os pedidos da perspetiva de um cliente.
/// </summary>
[ApiController]
[Route("api/clientes/{clienteId}/pedidos")]
public class PedidosClienteController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidosClienteController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    /// <summary>
    /// Cria um novo pedido a partir do carrinho do cliente (Checkout).
    /// </summary>
    /// <param name="clienteId">O ID do cliente que está a fazer a compra.</param>
    /// <param name="createDto">Contém o ID do endereço de entrega selecionado.</param>
    [HttpPost]
    public async Task<ActionResult<PedidoDto>> Create(int clienteId, [FromBody] CreatePedidoDto createDto)
    {
        try
        {
            var pedido = await _pedidoService.CreatePedidoAsync(clienteId, createDto);
            // Retorna 201 Created com o local do novo recurso e o objeto criado
            return CreatedAtAction(nameof(GetById), new { clienteId = clienteId, pedidoId = pedido.Id }, pedido);
        }
        catch (Exception ex)
        {
            // Retorna um erro 400 com a mensagem da exceção (ex: "Estoque insuficiente")
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Obtém o histórico de pedidos de um cliente.
    /// </summary>
    /// <param name="clienteId">O ID do cliente.</param>
    [HttpGet]
    public async Task<ActionResult<List<PedidoDto>>> Get(int clienteId) // Corrigido para retornar List<PedidoDto>
    {
        var pedidos = await _pedidoService.GetPedidosByClienteIdAsync(clienteId);
        return Ok(pedidos);
    }

    /// <summary>
    /// Obtém um pedido específico de um cliente.
    /// </summary>
    /// <param name="clienteId">O ID do cliente.</param>
    /// <param name="pedidoId">O ID do pedido.</param>
    [HttpGet("{pedidoId}")]
    public async Task<ActionResult<PedidoDto>> GetById(int clienteId, int pedidoId)
    {
        var pedido = await _pedidoService.GetPedidoByIdAsync(clienteId, pedidoId);
        if (pedido == null)
        {
            return NotFound();
        }
        return Ok(pedido);
    }
}
