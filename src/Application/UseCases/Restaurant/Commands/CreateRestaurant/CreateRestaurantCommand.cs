// using Application.Common.Interfaces;
// using Domain.Entities;
// using MediatR;

// namespace Application.UseCases.Restaurant.Commands.CreateRestaurant;

// public record CreateRestaurantCommand : IRequest<int>
// {
//     public required string Name { get; set; }
//     public required string Address { get; set; }
//     public string? ImageUrl { get; set; }
// }

// public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
// {
//     private readonly IDataContext _context;

//     public CreateRestaurantCommandHandler(IDataContext context)
//     {
//         _context = context;
//     }

//     public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
//     {
//         try
//         {
//             var entity = new RestaurantEntity(name: request.Name!)
//             {
//                 Slug = request.Slug
//             };

//             entity.AddDomainEvent(new RestaurantCreatedEvent(entity));

//             _context.Categories.Add(entity);
//             await _context.SaveChangesAsync(cancellationToken);

//             return entity.Id;
//         }
//         catch (Exception ex)
//         {
//             throw new Exception(ex.Message);
//         }
//     }
// }
