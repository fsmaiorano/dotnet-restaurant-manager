using Application.Common.Interfaces;
using Domain.Events.Promotion;
using MediatR;

namespace Application.UseCases.Promotion.Commands.DeletePromotionCommand;

public record DeletePromotionCommand(int Id) : IRequest;

public class DeletePromotionCommandHandler : IRequestHandler<DeletePromotionCommand>
{
    private readonly IDataContext _context;

    public DeletePromotionCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Promotions.FindAsync(request.Id) ??
                            throw new Exception("Promotion not found");

        _context.Promotions.Remove(entity);

        entity.AddDomainEvent(new PromotionDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
