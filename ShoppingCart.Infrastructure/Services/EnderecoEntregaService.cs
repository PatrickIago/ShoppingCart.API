using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Dto.EnderecoEntregaDtos;
using ShoppingCart.Application.Exceptions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Infrastructure.Services;
public class EnderecoEntregaService : IEnderecoEntregaService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public EnderecoEntregaService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EnderecoEntrega> Create(CreateEnderecoEntregaDto createEndereco)
    {
        var endereco = new EnderecoEntrega(
            createEndereco.Logradouro,
            createEndereco.Numero,
            createEndereco.Cidade,
            createEndereco.Estado,
            createEndereco.CEP,
            createEndereco.ClienteId
            );

        _context.Add(endereco);
        _context.SaveChanges();
        return endereco;

    }

    public async Task<bool> Delete(int id)
    {
        var endereco = await _context.EnderecosEntrega
            .FirstOrDefaultAsync(ed => ed.Id == id);

        if (endereco == null) return false;

        _context.Remove(endereco);
        _context.SaveChanges();
        return true;
    }

    public async Task<List<EnderecoEntregaDto>> Get()
    {
        var enderecos = await _context.EnderecosEntrega
            .Include(c => c.Cliente)
            .ToListAsync();

        return _mapper.Map<List<EnderecoEntregaDto>>(enderecos);

    }

    public async Task<List<EnderecoEntrega>> GetByCep(string cep)
    {
        var enderecos = await _context.EnderecosEntrega
                .Where(ed => ed.CEP
                .Replace("-", "") == cep.Replace("-", ""))
                .ToListAsync();

        return enderecos;
    }

    public async Task<EnderecoEntregaDto> GetById(int id)
    {
        var endereco = await _context.EnderecosEntrega
            .Include(c => c.Cliente)
            .FirstOrDefaultAsync(ed => ed.Id == id);

        if (endereco == null) throw new NotFoundException("O endereço não foi encontrado");

        return _mapper.Map<EnderecoEntregaDto>(endereco);
    }

    public async Task<EnderecoEntrega> Update(UpdateEnderecoEntregaDto updateEndereco)
    {
        var endereco = await _context.EnderecosEntrega
            .FirstOrDefaultAsync(ed => ed.Id == updateEndereco.Id);

        if (endereco == null) throw new NotFoundException("O endereço não foi encontrado");

        endereco.Atualizar(
            updateEndereco.Logradouro,
            updateEndereco.Numero,
            updateEndereco.Cidade,
            updateEndereco.Estado,
            updateEndereco.CEP,
            updateEndereco.ClienteId
            );

        return endereco;

    }
}
