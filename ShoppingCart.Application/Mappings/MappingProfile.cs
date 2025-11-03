using AutoMapper;
using ShoppingCart.Application.Dto.CarrinhoDtos;
using ShoppingCart.Application.Dto.ClienteDtos;
using ShoppingCart.Application.Dto.EnderecoEntregaDtos;
using ShoppingCart.Application.Dto.PedidoDtos; // Adicione este using
using ShoppingCart.Application.Dto.ProdutoDTOs;
using ShoppingCart.Domain.Models;
namespace ShoppingCart.Application.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // --- Mapeamentos de Produto ---
        CreateMap<CreateProdutoDto, Produto>();
        CreateMap<UpdateProdutoDto, Produto>();
        CreateMap<Produto, ProdutoDto>();

        // --- Mapeamentos de EnderecoEntrega ---
        CreateMap<CreateEnderecoEntregaDto, EnderecoEntrega>();
        CreateMap<UpdateEnderecoEntregaDto, EnderecoEntrega>();
        CreateMap<EnderecoEntrega, EnderecoEntregaDto>();

        // --- Mapeamento de Cliente ---
        CreateMap<Cliente, ClienteDto>();

        // --- Mapeamento de Carrinho ---
        CreateMap<ItemCarrinho, ItemCarrinhoDto>()
            .ForMember(dest => dest.NomeProduto, opt => opt.MapFrom(src => src.Produto.Nome))
            .ForMember(dest => dest.PrecoUnitario, opt => opt.MapFrom(src => src.Produto.Preco))
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.CalcularSubtotal()));

        CreateMap<Carrinho, CarrinhoDto>()
            .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.CalcularValorTotal()));

        // --- Mapeamento de Pedidos ---
        CreateMap<ItemPedido, ItemPedidoDto>()
            .ForMember(dest => dest.NomeProduto, opt => opt.MapFrom(src => src.Produto.Nome));

        CreateMap<Pedido, PedidoDto>()
            .ForMember(dest => dest.StatusPedido, opt => opt.MapFrom(src => src.StatusPedido.ToString()));
    }
}
