using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Dto.ProdutoDTOs;
using ShoppingCart.Application.Exceptions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Models;
namespace ShoppingCart.Infrastructure.Services;
public class ProdutoService : IProdutoService
{
    private readonly AppDbContext _context;
    public ProdutoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Produto> Create(CreateProdutoDto createProdutoDto)
    {
        var produto = new Produto(
            createProdutoDto.Nome,
            createProdutoDto.DescricaoProduto,
            createProdutoDto.Preco,
            createProdutoDto.Estoque
        );

        _context.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task<bool> Delete(int id)
    {
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null) throw new NotFoundException("O Produto não foi encontrado");

        _context.Remove(produto);
        _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Produto>> Get()
    {
        var produtos = await _context.Produtos.ToListAsync();
        return produtos;
    }

    public async Task<Produto> GetById(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null) throw new NotFoundException("O Produto não foi encontrado");

        return produto;
    }

    public async Task<List<Produto>> GetByName(string nome)
    {
        IQueryable<Produto> query = _context.Produtos;

        if (!string.IsNullOrEmpty(nome))
        {
            query = query.Where(produto => produto.Nome.ToUpper().Contains(nome.ToUpper()));
        }

        return query.ToList();
    }

    public async Task<Produto> Update(UpdateProdutoDto updateProdutoDto)
    {
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == updateProdutoDto.Id);

        if (produto == null) throw new NotFoundException("O Produto não foi encontrado");

        produto.Atualizar(
            updateProdutoDto.Nome,
            updateProdutoDto.DescricaoProduto,
            updateProdutoDto.Preco,
            updateProdutoDto.Estoque
        );

        await _context.SaveChangesAsync();
        return produto;
    }
}
