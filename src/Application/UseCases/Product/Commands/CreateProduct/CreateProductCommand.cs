﻿using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Product;
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

    public CreateProductCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new ProductEntity(name: request.Name, price: request.Price, restaurantId: request.RestaurantId)
            {
                ImageUrl = request.ImageUrl,
                Promotions = request.Promotions
            };

            _context.Products.Add(entity);

            entity.AddDomainEvent(new ProductCreatedEvent(entity));

            return _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
