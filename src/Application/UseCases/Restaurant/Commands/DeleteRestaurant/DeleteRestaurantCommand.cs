using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Restaurant;
using MediatR;

namespace Application.UseCases.Restaurant.Commands.DeleteRestaurant;

public record DeleteRestaurantCommand(int Id) : IRequest;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
{
    private readonly IDataContext _context;
    public DeleteRestaurantCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Restaurants.FindAsync(request.Id) ??
                           throw new Exception("Restaurant not found");

        _context.Restaurants.Remove(entity);

        entity.AddDomainEvent(new RestaurantDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
