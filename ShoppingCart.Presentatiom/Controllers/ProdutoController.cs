using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Dto.ProdutoDTOs;
using ShoppingCart.Application.Interfaces;
namespace ShoppingCart.API.Controllers;
/// <summary>
/// Controller para gerenciar os Produtos do catálogo.
/// </summary>
[ApiController]
[Route("api/[controller]")]

public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;
    private readonly IMapper _mapper;

    public ProdutoController(IProdutoService produtoService, IMapper mapper)
    {
        _produtoService = produtoService;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um novo produto ao catálogo.
    /// </summary>
    /// <param name="dto">Os dados do novo produto.</param>
    [HttpPost]
    public async Task<ActionResult<ProdutoDto>> Create([FromBody] CreateProdutoDto dto)
    {
        var produto = await _produtoService.Create(dto);
        var produtoDto = _mapper.Map<ProdutoDto>(produto);
        return CreatedAtAction(nameof(GetById), new { id = produtoDto.Id }, produtoDto);
    }

    /// <summary>
    /// Retorna todos os produtos do catálogo.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ProdutoDto>>> Get()
    {
        var produtos = await _produtoService.Get();
        var produtosDto = _mapper.Map<List<ProdutoDto>>(produtos);
        return Ok(produtosDto);
    }

    /// <summary>
    /// Retorna um produto por um ID específico.
    /// </summary>
    /// <param name="id">O ID do produto.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoDto>> GetById(int id)
    {
        var produto = await _produtoService.GetById(id);
        if (produto == null)
        {
            return NotFound();
        }

        var produtoDto = _mapper.Map<ProdutoDto>(produto);
        return Ok(produtoDto);
    }

    /// <summary>
    /// Pesquisa produtos pelo nome.
    /// </summary>
    /// <param name="nome">O nome a ser pesquisado.</param>
    [HttpGet("search")]
    public async Task<ActionResult<List<ProdutoDto>>> GetByName([FromQuery] string nome)
    {
        var produtos = await _produtoService.GetByName(nome);
        var produtosDto = _mapper.Map<List<ProdutoDto>>(produtos);
        return Ok(produtosDto);
    }

    /// <summary>
    /// Atualiza os dados de um produto existente.
    /// </summary>
    /// <param name="id">O ID do produto a ser atualizado.</param>
    /// <param name="updateDto">Os novos dados para o produto.</param>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProdutoDto>> Update([FromBody] UpdateProdutoDto updateDto)
    {
        var produto = await _produtoService.Update(updateDto);
        if (produto == null)
        {
            return NotFound();
        }

        var produtoDto = _mapper.Map<ProdutoDto>(produto);
        return Ok(produtoDto);
    }


    /// <summary>
    /// Remove um produto do catálogo.
    /// </summary>
    /// <param name="id">O ID do produto a ser removido.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _produtoService.Delete(id);
        if (!produto)
        {
            return NotFound();
        }

        return NoContent();
    }
}