using FluentValidation;

namespace Application.UseCases.Promotion.Commands.UpdatePromotionCommand;

public class UpdatePromotionCommandValidator : AbstractValidator<UpdatePromotionCommand>
{
    public UpdatePromotionCommandValidator()
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

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .GreaterThan(DateTime.MinValue);

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThan(DateTime.MinValue);

    }
}
