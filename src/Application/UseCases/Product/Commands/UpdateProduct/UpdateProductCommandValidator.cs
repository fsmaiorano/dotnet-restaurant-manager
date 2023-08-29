using FluentValidation;

namespace Application.UseCases.Product.Commands.UpdateProduct;
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(50);

        RuleFor(x => x.Price)
            .NotEmpty();

        RuleFor(x => x.RestaurantId)
            .NotEmpty();
    }
}
