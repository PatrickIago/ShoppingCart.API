using FluentValidation;
using ShoppingCart.Application.Dto.EnderecoEntregaDtos;

namespace ShoppingCart.Application.Validators.EnderecoEntregaValidator;
public class UpdateEnderecoValidator : AbstractValidator<UpdateEnderecoEntregaDto>
{
    public UpdateEnderecoValidator()
    {
        RuleFor(ed => ed.Id)
            .NotEmpty()
            .WithMessage("O Id deve ser informado");
        RuleFor(ed => ed.Logradouro)
            .NotEmpty()
            .WithMessage("O Logradouro deve ser informado")
            .MaximumLength(50)
            .WithMessage("O logradouro deve ter no maximo 50 caracteres");
        RuleFor(ed => ed.Numero)
            .NotEmpty()
            .WithMessage("O Numero deve ser informado");
        RuleFor(ed => ed.Cidade)
            .NotEmpty()
            .WithMessage("A Cidade deve ser informada");
        RuleFor(ed => ed.Estado)
           .NotEmpty()
           .WithMessage("O estado deve ser informado");
        RuleFor(ed => ed.CEP)
          .NotEmpty()
          .WithMessage("O CEP deve ser informado");
        RuleFor(ed => ed.ClienteId)
          .NotEmpty()
          .WithMessage("O Id do cliente deve ser informado");
    }
}
