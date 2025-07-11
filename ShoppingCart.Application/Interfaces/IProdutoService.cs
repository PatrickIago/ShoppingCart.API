using ShoppingCart.Application.Dto.ProdutoDTOs;
using ShoppingCart.Domain.Models;
namespace ShoppingCart.Application.Interfaces;
public interface IProdutoService
{
    Task<List<Produto>> Get();
    Task<Produto> GetById(int id);
    Task<List<Produto>> GetByName(string nome);
    Task<Produto> Create(CreateProdutoDto createProdutoDto);
    Task<Produto> Update(UpdateProdutoDto updateProdutoDto);
    Task<bool> Delete(int id);
}