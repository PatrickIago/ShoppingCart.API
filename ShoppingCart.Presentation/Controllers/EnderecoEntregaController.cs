using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Dto.EnderecoEntregaDtos;
using ShoppingCart.Application.Interfaces;
namespace ShoppingCart.API.Controllers;
/// <summary>
/// Controller para gerenciar os endereços de entrega dos clientes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
// [Authorize] 
public class EnderecoEntregaController : ControllerBase
{
    private readonly IEnderecoEntregaService _enderecoService;
    private readonly IMapper _mapper;

    public EnderecoEntregaController(IEnderecoEntregaService enderecoService, IMapper mapper)
    {
        _enderecoService = enderecoService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retorna todos os endereços (geralmente, seria filtrado por usuário).
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<EnderecoEntregaDto>>> Get()
    {
        var enderecos = await _enderecoService.Get();
        var enderecosDto = _mapper.Map<List<EnderecoEntregaDto>>(enderecos);
        return Ok(enderecosDto);
    }

    /// <summary>
    /// Retorna um endereço pelo seu ID.
    /// </summary>
    /// <param name="id">O ID do endereço.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<EnderecoEntregaDto>> GetById(int id)
    {
        var endereco = await _enderecoService.GetById(id);

        if (endereco == null)
        {
            return NotFound($"Endereço com ID {id} não encontrado.");
        }

        var enderecoDto = _mapper.Map<EnderecoEntregaDto>(endereco);
        return Ok(enderecoDto);
    }

    /// <summary>
    /// Busca endereços por um CEP específico.
    /// </summary>
    /// <param name="cep">O CEP a ser pesquisado.</param>
    [HttpGet("cep/{cep}")]
    public async Task<ActionResult<List<EnderecoEntregaDto>>> GetByCep([FromQuery] string cep)
    {
        var enderecos = await _enderecoService.GetByCep(cep);
        var enderecosDto = _mapper.Map<List<EnderecoEntregaDto>>(enderecos);
        return Ok(enderecosDto);
    }

    /// <summary>
    /// Cria um novo endereço de entrega.
    /// </summary>
    /// <param name="createDto">Os dados do novo endereço.</param>
    [HttpPost]
    public async Task<ActionResult<EnderecoEntregaDto>> Create([FromBody] CreateEnderecoEntregaDto createDto)
    {
        var endereco = await _enderecoService.Create(createDto);
        return CreatedAtAction(nameof(GetById), new { id = endereco.Id }, endereco);
    }

    /// <summary>
    /// Atualiza um endereço existente.
    /// </summary>
    /// <param name="id">O ID do endereço a ser atualizado.</param>
    /// <param name="updateDto">Os novos dados para o endereço.</param>
    [HttpPut("{id}")]
    public async Task<ActionResult<EnderecoEntregaDto>> Update(int id, [FromBody] UpdateEnderecoEntregaDto updateDto)
    {
        var endereco = await _enderecoService.Update(updateDto);
        if (endereco == null)
            return NotFound();

        return Ok(endereco);
    }

    /// <summary>
    /// Remove um endereço pelo seu ID.
    /// </summary>
    /// <param name="id">O ID do endereço a ser removido.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _enderecoService.Delete(id);
        if (!deleted)
        {
            return NotFound($"Endereço com ID {id} não encontrado.");
        }

        return NoContent();
    }
}