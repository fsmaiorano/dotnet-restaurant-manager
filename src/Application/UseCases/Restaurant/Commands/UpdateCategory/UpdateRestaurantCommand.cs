using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Restaurant.Commands.UpdateRestaurant;

public record UpdateRestaurantCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? ImageUrl { get; set; }
}

public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
{
    private readonly IDataContext _context;

    public UpdateRestaurantCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Restaurants.FindAsync(new object[] { request.Id, }, cancellationToken);

        if (entity == null)
        {
            // throw new NotFoundException(nameof(UserEntity), request.Id);
        }

        entity.Name = request.Name ?? entity.Name;


        await _context.SaveChangesAsync(cancellationToken);
    }
}
