using ShoppingCart.Application.Dto.ClienteDtos;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.Interfaces;
public interface IClienteService
{
    Task<List<ClienteDto>> Get();
    Task<ClienteDto> GetById(int id);
    Task<List<Cliente>> GetByName(string nome);
    Task<Cliente> Create(CreateClienteDto cliente);
    Task<Cliente> Update(UpdateClienteDto cliente);
    Task<bool> Delete(int id);
}
