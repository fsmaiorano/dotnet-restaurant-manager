using Domain.Common;

namespace Domain.Entities;

public class PromotionEntity : BaseAuditableEntity
{
    public int ProductId { get; set; }
    public ProductEntity? Product { get; set; }
    public required string Description { get; set; }
    public required decimal PromotionalPrice { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required bool IsActive { get; set; }
    // public required List<string> DaysAndTimes { get; set; }
}
