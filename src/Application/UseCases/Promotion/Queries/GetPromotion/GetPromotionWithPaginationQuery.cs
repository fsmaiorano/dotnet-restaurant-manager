using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Promotion.Queries.GetPromotionWithPaginationQuery;

public record GetPromotionWithPaginationQuery : IRequest<PaginatedList<PromotionEntity>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPromotionWhiPaginationQueryHandler : IRequestHandler<GetPromotionWithPaginationQuery, PaginatedList<PromotionEntity>>
{
    private readonly IDataContext _context;

    public GetPromotionWhiPaginationQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<PromotionEntity>> Handle(GetPromotionWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Promotions
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
