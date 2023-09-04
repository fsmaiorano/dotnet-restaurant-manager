using Application.UseCases.Promotion.Commands.CreatePromotion;
using FluentValidation;

namespace Application.UseCases.Promotion.Commands.CreatePromotionCommandValidator;

public class CreatePromotionCommandValidator : AbstractValidator<CreatePromotionCommand>
{
    public CreatePromotionCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.PromotionalPrice)
            .NotEmpty()
            .GreaterThan(0);

        // RuleFor(x => x.DaysAndTimes)
        //     .NotEmpty()
        //     .MaximumLength(100);
    }
}
