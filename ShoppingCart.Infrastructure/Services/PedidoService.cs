using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Dto.PedidoDtos;
using ShoppingCart.Application.Exceptions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Models;
namespace ShoppingCart.Infrastructure.Services;
public class PedidoService : IPedidoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PedidoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PedidoDto> CreatePedidoAsync(int clienteId, CreatePedidoDto createPedidoDto)
    {
        // Usamos uma transação para garantir que todas as operações sejam bem-sucedidas ou nenhuma o seja.
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // 1. Carregar as entidades necessárias
            var cliente = await _context.Clientes.FindAsync(clienteId);
            if (cliente == null) throw new KeyNotFoundException("Cliente não encontrado.");

            var endereco = await _context.EnderecosEntrega.FirstOrDefaultAsync(e => e.Id == createPedidoDto.EnderecoEntregaId && e.ClienteId == clienteId);
            if (endereco == null) throw new KeyNotFoundException("Endereço de entrega inválido ou não pertence a este cliente.");

            var carrinho = await _context.Carrinhos
                .Include(c => c.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);

            if (carrinho == null || carrinho.Itens.Count == 0)
                throw new InvalidOperationException("O carrinho está vazio.");

            var pedido = new Pedido(cliente, endereco, carrinho);


            foreach (var item in pedido.Itens)
            {
                var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                if (produto.Estoque < item.Quantidade)
                {
                    // Se um produto não tiver estoque, desfazemos a transação.
                    throw new InvalidOperationException($"Estoque insuficiente para o produto '{produto.Nome}'.");
                }
                produto.RemoverDoEstoque(item.Quantidade);
            }

            // Limpar o carrinho
            carrinho.Limpar();

            // Adicionar o novo pedido ao contexto
            _context.Pedidos.Add(pedido);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return _mapper.Map<PedidoDto>(pedido);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<PedidoDto>> GetPedidosByClienteIdAsync(int clienteId)
    {
        var pedidos = await _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .Include(p => p.EnderecoEntrega)
            .Where(p => p.ClienteId == clienteId)
            .ToListAsync();

        return _mapper.Map<List<PedidoDto>>(pedidos);
    }

    public async Task<PedidoDto> GetPedidoByIdAsync(int clienteId, int pedidoId)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .Include(p => p.EnderecoEntrega)
            .FirstOrDefaultAsync(p => p.Id == pedidoId && p.ClienteId == clienteId);

        return _mapper.Map<PedidoDto>(pedido);
    }

    public async Task<PedidoDto> MarcarPedidoComoPagoAsync(int pedidoId)
    {
        var pedido = await _context.Pedidos.FindAsync(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado.");

        pedido.MarcarComoPago();

        await _context.SaveChangesAsync();
        return _mapper.Map<PedidoDto>(pedido);
    }

    public async Task<PedidoDto> MarcarPedidoComoEnviadoAsync(int pedidoId)
    {
        var pedido = await _context.Pedidos.FindAsync(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado.");

        pedido.MarcarComoEnviado();

        await _context.SaveChangesAsync();
        return _mapper.Map<PedidoDto>(pedido);
    }

    public async Task<PedidoDto> CancelarPedidoAsync(int pedidoId)
    {
        var pedido = await _context.Pedidos.FindAsync(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado.");

        pedido.Cancelar();

        await _context.SaveChangesAsync();
        return _mapper.Map<PedidoDto>(pedido);
    }
}