using Application.UseCases.Product.Commands.CreateProduct;
using FluentValidation;

namespace Application.UseCases.Product.Commands.CreateProductCommandValidator;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.RestaurantId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
