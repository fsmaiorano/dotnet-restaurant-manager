using FluentValidation;

namespace Application.UseCases.Restaurant.Commands.CreateRestaurant;
public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    public CreateRestaurantCommandValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(250);
    }
}
