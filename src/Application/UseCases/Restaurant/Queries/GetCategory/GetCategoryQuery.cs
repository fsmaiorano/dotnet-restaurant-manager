using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Restaurant.Queries.GetRestaurant;

public record GetRestaurantQuery : IRequest<List<RestaurantEntity>>;

public class GetRestaurantQueryHandler : IRequestHandler<GetRestaurantQuery, List<RestaurantEntity>>
{
    private readonly IDataContext _context;

    public GetRestaurantQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<List<RestaurantEntity>> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
    {
        return await _context.Restaurants.ToListAsync(cancellationToken);
    }
}
