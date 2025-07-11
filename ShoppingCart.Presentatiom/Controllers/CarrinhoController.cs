using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Dto.CarrinhoDtos;
using ShoppingCart.Application.Interfaces;
namespace ShoppingCart.API.Controllers;
/// <summary>
/// Controller para gerir as ações do carrinho de compras de um cliente.
/// </summary>
[ApiController]
[Route("api/clientes/{clienteId}/carrinho")]
public class CarrinhoController : ControllerBase
{
    private readonly ICarrinhoService _carrinhoService;

    public CarrinhoController(ICarrinhoService carrinhoService)
    {
        _carrinhoService = carrinhoService;
    }

    /// <summary>
    /// Obtém o carrinho de compras de um cliente específico.
    /// </summary>
    /// <param name="clienteId">O ID do cliente.</param>
    [HttpGet]
    public async Task<ActionResult<CarrinhoDto>> Get(int clienteId)
    {
        var carrinho = await _carrinhoService.GetCarrinhoByClienteIdAsync(clienteId);
        return Ok(carrinho);
    }

    /// <summary>
    /// Adiciona um item ao carrinho de um cliente.
    /// </summary>
    /// <param name="clienteId">O ID do cliente.</param>
    /// <param name="itemDto">Os dados do item a ser adicionado.</param>
    [HttpPost("itens")]
    public async Task<ActionResult<CarrinhoDto>> AdicionarItem(int clienteId, [FromBody] AdicionarItemDto itemDto)
    {
        var carrinhoAtualizado = await _carrinhoService.AdicionarItemAsync(clienteId, itemDto);
        return Ok(carrinhoAtualizado);
    }

    /// <summary>
    /// Atualiza a quantidade de um item no carrinho de um cliente.
    /// </summary>
    /// <param name="clienteId">O ID do cliente.</param>
    /// <param name="produtoId">O ID do produto a ser atualizado.</param>
    /// <param name="itemDto">Os novos dados de quantidade.</param>
    [HttpPut("itens/{produtoId}")]
    public async Task<ActionResult<CarrinhoDto>> AtualizarItem(int clienteId, int produtoId, [FromBody] AtualizarItemDto itemDto)
    {
        var carrinhoAtualizado = await _carrinhoService.AtualizarItemAsync(clienteId, produtoId, itemDto);
        return Ok(carrinhoAtualizado);
    }

    /// <summary>
    /// Remove um item do carrinho de um cliente.
    /// </summary>
    /// <param name="clienteId">O ID do cliente.</param>
    /// <param name="produtoId">O ID do produto a ser removido.</param>
    [HttpDelete("itens/{produtoId}")]
    public async Task<ActionResult<CarrinhoDto>> RemoverItem(int clienteId, int produtoId)
    {
        var carrinhoAtualizado = await _carrinhoService.RemoverItemAsync(clienteId, produtoId);
        return Ok(carrinhoAtualizado);
    }

    /// <summary>
    /// Esvazia o carrinho de um cliente, removendo todos os itens.
    /// </summary>
    /// <param name="clienteId">O ID do cliente.</param>
    [HttpDelete]
    public async Task<IActionResult> LimparCarrinho(int clienteId)
    {
        await _carrinhoService.LimparCarrinhoAsync(clienteId);
        return NoContent();
    }
}
