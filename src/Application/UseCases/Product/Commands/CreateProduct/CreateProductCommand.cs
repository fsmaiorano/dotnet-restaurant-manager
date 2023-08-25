using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Product.Commands.CreateProduct;

public record CreateProductCommand : IRequest<int>
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public required int RestaurantId { get; set; }
    public IList<PromotionEntity>? Promotions { get; set; }

}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IDataContext _context;

    public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new ProductEntity
            {
                Name = request.Name,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                RestaurantId = request.RestaurantId,
                Promotions = request.Promotions
            };

            _context.Products.Add(entity);

            return _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
