using Domain.Common;

namespace Domain.Entities;

public class RestaurantEntity: BaseAuditableEntity
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public string? ImageUrl { get; set; }
}
