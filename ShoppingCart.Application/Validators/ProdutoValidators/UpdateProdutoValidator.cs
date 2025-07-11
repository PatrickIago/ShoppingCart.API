using FluentValidation;
using ShoppingCart.Application.Dto.ProdutoDTOs;
namespace ShoppingCart.Application.Validators.ProdutoValidators;

public class UpdateProdutoValidator : AbstractValidator<UpdateProdutoDto>
{
    public UpdateProdutoValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("O Id deve ser informado");
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
            .WithMessage("Não pode ser adicionar um produto que não tenha estoque");
    }
}
