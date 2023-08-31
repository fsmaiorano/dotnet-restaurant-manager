using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Product.Queries.GetProductWithPaginationQuery;

public record GetProductWithPaginationQuery : IRequest<PaginatedList<ProductEntity>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetProductWhiPaginationQueryHandler : IRequestHandler<GetProductWithPaginationQuery, PaginatedList<ProductEntity>>
{
    private readonly IDataContext _context;

    public GetProductWhiPaginationQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ProductEntity>> Handle(GetProductWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
