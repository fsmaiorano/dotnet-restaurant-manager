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
        throw new NotImplementedException();
    }
}
