using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entities;

public class ProductEntity : BaseAuditableEntity
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public required int RestaurantId { get; set; }
    public virtual RestaurantEntity? Restaurant { get; set; }
    public IList<PromotionEntity>? Promotions { get; set; }

    [SetsRequiredMembers]
    public ProductEntity(string name, decimal price, string? imageUrl = null, int restaurantId = 0)
    {
        Name = name;
        Price = price;
        ImageUrl = imageUrl;
        RestaurantId = restaurantId;
        Promotions = new List<PromotionEntity>();
    }
}
