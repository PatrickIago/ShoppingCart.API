using FluentValidation;
using ShoppingCart.Application.Dto.ClienteDtos;
namespace ShoppingCart.Application.Validators.ClienteValidators;
public class UpdateClienteValidator : AbstractValidator<UpdateClienteDto>
{
    public UpdateClienteValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("O Id do cliente deve ser informado");
        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("O nome deve ser preenchido");
        RuleFor(c => c.Nome)
            .MaximumLength(50)
            .WithMessage("O nome não deve ter mais de 50 caracteres");
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("O email deve ser informado");
    }
}
