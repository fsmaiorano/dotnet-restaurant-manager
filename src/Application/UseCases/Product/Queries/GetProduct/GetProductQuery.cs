using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Product.Queries.GetProduct;
public record GetProductQuery : IRequest<List<ProductEntity>>;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<ProductEntity>>
{
    private readonly IDataContext _context;

    public GetProductQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public Task<List<ProductEntity>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return _context.Products.ToListAsync(cancellationToken);
    }
}
