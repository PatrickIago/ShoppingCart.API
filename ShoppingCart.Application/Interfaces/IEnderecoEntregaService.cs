using ShoppingCart.Application.Dto.EnderecoEntregaDtos;
using ShoppingCart.Domain.Models;
namespace ShoppingCart.Application.Interfaces;
public interface IEnderecoEntregaService
{
    Task<List<EnderecoEntregaDto>> Get();
    Task<EnderecoEntregaDto> GetById(int id);
    Task<EnderecoEntrega> Create(CreateEnderecoEntregaDto createEndereco);
    Task<EnderecoEntrega> Update(UpdateEnderecoEntregaDto updateEndereco);
    Task<bool> Delete(int id);
    Task<List<EnderecoEntrega>> GetByCep(string cep);
}
