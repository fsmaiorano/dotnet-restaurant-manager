using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entities;

public class RestaurantEntity : BaseAuditableEntity
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public string? ImageUrl { get; set; }

    public IList<ProductEntity>? Products { get; set; }

    [SetsRequiredMembers]
    public RestaurantEntity(string name, string address, string? imageUrl = null)
    {
        Name = name;
        Address = address;
        ImageUrl = imageUrl;
    }
}
