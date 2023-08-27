using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Product;

public class ProductCreatedEvent : BaseEvent
{
    public ProductCreatedEvent(ProductEntity product)
    {
        Product = product;
    }

    public ProductEntity Product { get; set; }
}
