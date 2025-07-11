using FluentValidation;
using ShoppingCart.Application.Dto.CarrinhoDtos;
namespace ShoppingCart.Application.Validators.CarrinhoValidators;
public class AtualizarItemValidator : AbstractValidator<AtualizarItemDto>
{
    public AtualizarItemValidator()
    {
        RuleFor(at => at.Quantidade)
            .NotEmpty()
            .WithMessage("A quantidade não pode estar vazia")
            .GreaterThan(0)
            .WithMessage("A quantidade informada deve ser maior que 0");
    }
}
