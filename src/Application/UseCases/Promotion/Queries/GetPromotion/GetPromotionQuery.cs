using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Promotion.Queries.GetPromotion;
public record GetPromotionQuery : IRequest<List<PromotionEntity>>;

public class GetPromotionQueryHandler : IRequestHandler<GetPromotionQuery, List<PromotionEntity>>
{
    private readonly IDataContext _context;

    public GetPromotionQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public Task<List<PromotionEntity>> Handle(GetPromotionQuery request, CancellationToken cancellationToken)
    {
        return _context.Promotions.ToListAsync(cancellationToken);
    }
}
