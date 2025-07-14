using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Dto.ClienteDtos;
using ShoppingCart.Application.Interfaces;
namespace ShoppingCart.API.Controllers;
/// <summary>
/// Controller para gerenciar os dados de perfis de Clientes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly IMapper _mapper;

    public ClienteController(IClienteService clienteService, IMapper mapper)
    {
        _clienteService = clienteService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retorna todos os perfis de clientes.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ClienteDto>>> Get()
    {
        var clientes = await _clienteService.Get();
        var clientesDto = _mapper.Map<List<ClienteDto>>(clientes);
        return Ok(clientesDto);
    }
    /// <summary>
    /// Adiciona um novo cliente
    /// </summary>
    /// <param name="createClienteDto">Os dados do novo cliente</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Create([FromBody] CreateClienteDto createClienteDto)
    {
        var cliente = await _clienteService.Create(createClienteDto);
        var clienteDto = _mapper.Map<ClienteDto>(cliente);

        return CreatedAtAction(nameof(GetById), new { id = clienteDto.Id }, clienteDto);
    }

    /// <summary>
    /// Retorna um perfil de cliente por um ID específico.
    /// </summary>
    /// <param name="id">O ID do cliente.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDto>> GetById(int id)
    {
        var cliente = await _clienteService.GetById(id);
        if (cliente == null)
        {
            return NotFound();
        }

        var clienteDto = _mapper.Map<ClienteDto>(cliente);
        return Ok(clienteDto);
    }

    /// <summary>
    /// Pesquisa perfis de cliente pelo nome.
    /// </summary>
    /// <param name="nome">O nome a ser pesquisado.</param>
    [HttpGet("search")]
    public async Task<ActionResult<List<ClienteDto>>> GetByName([FromQuery] string nome)
    {
        var clientes = await _clienteService.GetByName(nome);
        var clientesDto = _mapper.Map<List<ClienteDto>>(clientes);
        return Ok(clientesDto);
    }

    /// <summary>
    /// Atualiza os dados de um perfil de cliente.
    /// </summary>
    /// <param name="id">O ID do cliente a ser atualizado.</param>
    /// <param name="updateDto">Os novos dados para o cliente.</param>
    [HttpPut("{id}")]
    public async Task<ActionResult<ClienteDto>> Update([FromBody] UpdateClienteDto updateDto)
    {
        var cliente = await _clienteService.Update(updateDto);
        if (cliente == null)
        {
            return NotFound();
        }

        var clienteDto = _mapper.Map<ClienteDto>(cliente);
        return Ok(clienteDto);
    }

    /// <summary>
    /// Remove um perfil de cliente.
    /// </summary>
    /// <param name="id">O ID do cliente a ser removido.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _clienteService.Delete(id);
        if (!cliente)
        {
            return NotFound();
        }

        return NoContent();
    }

}