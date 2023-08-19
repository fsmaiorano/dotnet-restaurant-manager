using Domain.Common;

namespace Domain.Entities;

public class PromotionEntity : BaseAuditableEntity
{
    public ProductEntity? Product { get; set; }
    public required string Description { get; set; }
    public required decimal PromotionalPrice { get; set; }
    public required string[] DaysAndTimes { get; set; }
}
