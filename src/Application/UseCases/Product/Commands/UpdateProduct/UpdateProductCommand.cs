using System.Diagnostics.CodeAnalysis;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Product.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public required int RestaurantId { get; set; }
    public IList<PromotionEntity>? Promotions { get; set; }

    [SetsRequiredMembers]
    public UpdateProductCommand(int id, string name, decimal price, int restaurantId)
    {
        Id = id;
        Name = name;
        Price = price;
        RestaurantId = restaurantId;
    }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IDataContext _context;

    public UpdateProductCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FindAsync(new object[] { request.Id, }, cancellationToken) ?? throw new NotFoundException(nameof(ProductEntity), request.Id);
        entity.Name = request.Name ?? entity.Name;
        entity.Price = request.Price;
        entity.ImageUrl = request.ImageUrl ?? entity.ImageUrl;
        entity.RestaurantId = request.RestaurantId;
        entity.Promotions = request.Promotions ?? entity.Promotions;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
