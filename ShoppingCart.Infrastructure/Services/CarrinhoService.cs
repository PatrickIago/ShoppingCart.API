using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Dto.CarrinhoDtos;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Models;
namespace ShoppingCart.Infrastructure.Services;
public class CarrinhoService : ICarrinhoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CarrinhoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CarrinhoDto> GetCarrinhoByClienteIdAsync(int clienteId)
    {
        var carrinho = await ObterOuCriarCarrinhoComItensAsync(clienteId);
        return _mapper.Map<CarrinhoDto>(carrinho);
    }

    public async Task<CarrinhoDto> AdicionarItemAsync(int clienteId, AdicionarItemDto itemDto)
    {
        var carrinho = await ObterOuCriarCarrinhoComItensAsync(clienteId);
        var produto = await _context.Produtos.FindAsync(itemDto.ProdutoId);

        if (produto == null)
            throw new KeyNotFoundException("Produto não encontrado.");

        if (produto.Estoque < itemDto.Quantidade)
            throw new InvalidOperationException("Estoque insuficiente.");

        carrinho.AdicionarItem(produto, itemDto.Quantidade);

        await _context.SaveChangesAsync();
        return _mapper.Map<CarrinhoDto>(carrinho);
    }

    public async Task<CarrinhoDto> AtualizarItemAsync(int clienteId, int produtoId, AtualizarItemDto itemDto)
    {
        var carrinho = await ObterOuCriarCarrinhoComItensAsync(clienteId);
        carrinho.AlterarQuantidadeItem(produtoId, itemDto.Quantidade);

        await _context.SaveChangesAsync();
        return _mapper.Map<CarrinhoDto>(carrinho);
    }

    public async Task<CarrinhoDto> RemoverItemAsync(int clienteId, int produtoId)
    {
        var carrinho = await ObterOuCriarCarrinhoComItensAsync(clienteId);
        carrinho.RemoverItem(produtoId);

        await _context.SaveChangesAsync();
        return _mapper.Map<CarrinhoDto>(carrinho);
    }

    public async Task LimparCarrinhoAsync(int clienteId)
    {
        var carrinho = await ObterOuCriarCarrinhoComItensAsync(clienteId);
        carrinho.Limpar();

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Método privado para obter o carrinho do cliente, incluindo os seus itens e produtos.
    /// Se o carrinho não existir, ele é criado.
    /// </summary>
    private async Task<Carrinho> ObterOuCriarCarrinhoComItensAsync(int clienteId)
    {
        var carrinho = await _context.Carrinhos
            .Include(c => c.Itens)
            .ThenInclude(i => i.Produto) // Inclui os dados do produto em cada item
            .FirstOrDefaultAsync(c => c.ClienteId == clienteId);

        if (carrinho == null)
        {
            // Verifica se o cliente existe antes de criar um carrinho para ele
            var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == clienteId);
            if (!clienteExiste)
            {
                throw new KeyNotFoundException($"Cliente com ID {clienteId} não encontrado.");
            }

            carrinho = new Carrinho(clienteId);
            _context.Carrinhos.Add(carrinho);
        }

        return carrinho;
    }
}