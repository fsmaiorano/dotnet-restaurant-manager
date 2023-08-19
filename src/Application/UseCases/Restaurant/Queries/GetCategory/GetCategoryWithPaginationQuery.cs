using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Restaurant.Queries.GetRestaurant;

public record GetRestaurantWithPaginationQuery : IRequest<PaginatedList<RestaurantEntity>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

};

public class GetRestaurantWithPaginationHandler : IRequestHandler<GetRestaurantWithPaginationQuery, PaginatedList<RestaurantEntity>>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;

    public GetRestaurantWithPaginationHandler(IDataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<RestaurantEntity>> Handle(GetRestaurantWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Restaurants
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
