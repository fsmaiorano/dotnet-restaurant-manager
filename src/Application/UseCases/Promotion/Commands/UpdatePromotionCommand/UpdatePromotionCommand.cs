using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Promotion.Commands.UpdatePromotionCommand;

public record UpdatePromotionCommand : IRequest
{
    public int Id { get; set; }
    public required int ProductId { get; set; }
    public required string Description { get; set; }
    public required decimal PromotionalPrice { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
}

public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand>
{
    private readonly IDataContext _context;

    public UpdatePromotionCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Promotions.FindAsync(new object[] { request.Id, }, cancellationToken)
                                ?? throw new NotFoundException(nameof(PromotionEntity), request.Id);
        entity.ProductId = request.ProductId == 0 ? entity.ProductId : request.ProductId;
        entity.Description = request.Description ?? entity.Description;
        entity.PromotionalPrice = request.PromotionalPrice == 0 ? entity.PromotionalPrice : request.PromotionalPrice;
        entity.StartDate = request.StartDate == DateTime.MinValue ? entity.StartDate : request.StartDate;
        entity.EndDate = request.EndDate == DateTime.MinValue ? entity.EndDate : request.EndDate;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
