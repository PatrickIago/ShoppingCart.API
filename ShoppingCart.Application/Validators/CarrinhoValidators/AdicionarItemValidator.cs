using FluentValidation;
using ShoppingCart.Application.Dto.CarrinhoDtos;

namespace ShoppingCart.Application.Validators.CarrinhoValidators;
public class AdicionarItemValidator : AbstractValidator<AdicionarItemDto>
{
    public AdicionarItemValidator()
    {
        RuleFor(ad => ad.ProdutoId)
            .NotEmpty()
            .WithMessage("O Id do produto não pode estar vazio");
        RuleFor(ad => ad.Quantidade)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("A quantidade informada deve ser maior que 0");
    }
}
