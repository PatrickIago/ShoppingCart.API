using FluentValidation;
using ShoppingCart.Application.Dto.PedidoDtos;
namespace ShoppingCart.Application.Validators.PedidoValidators;
public class CreatePedidoValidator : AbstractValidator<CreatePedidoDto>
{
    public CreatePedidoValidator()
    {
        RuleFor(p => p.EnderecoEntregaId)
            .NotEmpty()
            .WithMessage("O Id do endereço deve ser informado");
    }
}
