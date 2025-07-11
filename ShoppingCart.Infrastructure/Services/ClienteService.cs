using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Dto.ClienteDtos;
using ShoppingCart.Application.Exceptions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Models;
namespace ShoppingCart.Infrastructure.Services;
public class ClienteService : IClienteService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ClienteService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Cliente> Create(CreateClienteDto createCliente)
    {
        var cliente = new Cliente(
            createCliente.Nome,
            createCliente.Email
            );

        _context.Add(cliente);
        await _context.SaveChangesAsync();

        return cliente;

    }

    public async Task<bool> Delete(int id)
    {
        var cliente = await _context.Clientes
             .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente == null) throw new NotFoundException("O cliente não foi encontrado");

        _context.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;

    }

    public async Task<List<ClienteDto>> Get()
    {
        var clientes = await _context.Clientes
            .Include(c => c.Enderecos)
            .ToListAsync();

        return _mapper.Map<List<ClienteDto>>(clientes);
    }

    public async Task<ClienteDto> GetById(int id)
    {
        var cliente = await _context.Clientes
            .Include(c => c.Enderecos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente == null) throw new NotFoundException("O cliente não foi encontrado");

        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<List<Cliente>> GetByName(string nome)
    {
        IQueryable<Cliente> query = _context.Clientes;

        if (!string.IsNullOrEmpty(nome))
        {
            query = query.Where(cliente => cliente.Nome.ToUpper().Contains(nome.ToUpper()));
        }

        return query.ToList();

    }

    public async Task<Cliente> Update(UpdateClienteDto updateCliente)
    {
        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(c => c.Id == updateCliente.Id);

        if (cliente == null) throw new NotFoundException("O cliente não foi encontrado");

        cliente.Atualizar(
            updateCliente.Nome,
            updateCliente.Email
            );

        return cliente;
    }
}
