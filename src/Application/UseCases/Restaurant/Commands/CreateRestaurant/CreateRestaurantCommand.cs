using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Restaurant;
using MediatR;

namespace Application.UseCases.Restaurant.Commands.CreateRestaurant;

public record CreateRestaurantCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? ImageUrl { get; set; }
}

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
{
    private readonly IDataContext _context;

    public CreateRestaurantCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new RestaurantEntity(name: request.Name!, address: request.Address!, imageUrl: request.ImageUrl ?? null) 
                             ?? throw new Exception("Restaurant not found");
            entity.AddDomainEvent(new RestaurantCreatedEvent(entity));

            _context.Restaurants.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
