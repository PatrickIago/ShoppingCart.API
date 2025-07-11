using FluentValidation;
using ShoppingCart.Application.Dto.ProdutoDTOs;
namespace ShoppingCart.Application.Validators.ProdutoValidators;
public class CreateProdutoValidator : AbstractValidator<CreateProdutoDto>
{
    public CreateProdutoValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithMessage("O nome do produto deve ser informado");
        RuleFor(p => p.DescricaoProduto)
            .NotEmpty()
            .WithMessage("A descrição deve ser informada");
        RuleFor(p => p.Preco)
            .GreaterThan(0)
            .WithMessage("O produto não pode ser inserido com valor zerado");
        RuleFor(p => p.Estoque)
            .GreaterThan(0)
            .WithMessage("Um produto não pode ser adicionada sem estoque");
    }
}
