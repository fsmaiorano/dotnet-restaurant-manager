using Application.Common.Interfaces;
using FluentValidation;

namespace Application.UseCases.Restaurant.Commands.UpdateRestaurant;
public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {

        RuleFor(x => x.Id)
       .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(50);

        RuleFor(x => x.Address)
            .MaximumLength(250);
    }
}
