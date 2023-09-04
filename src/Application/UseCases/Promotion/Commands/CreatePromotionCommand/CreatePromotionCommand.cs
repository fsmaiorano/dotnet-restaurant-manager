using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Promotion;
using MediatR;

namespace Application.UseCases.Promotion.Commands.CreatePromotion;

public record CreatePromotionCommand : IRequest<int>
{
    public required int ProductId { get; set; }
    public required string Description { get; set; }
    public required decimal PromotionalPrice { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
}

public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, int>
{
    private readonly IDataContext _context;

    public CreatePromotionCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public Task<int> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new PromotionEntity(productId: request.ProductId,
                                    description: request.Description,
                                    promotionalPrice: request.PromotionalPrice,
                                    startDate: request.StartDate,
                                    endDate: request.EndDate
                                    );

            _context.Promotions.Add(entity);

            entity.AddDomainEvent(new PromotionCreatedEvent(entity));

            return _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}

