using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entities;

public class PromotionEntity : BaseAuditableEntity
{
    public required int ProductId { get; set; }
    public virtual ProductEntity? Product { get; set; }
    public required string Description { get; set; }
    public required decimal PromotionalPrice { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    // public required List<string> DaysAndTimes { get; set; }

    [SetsRequiredMembers]
    public PromotionEntity(int productId, string description, decimal promotionalPrice, DateTime startDate, DateTime endDate)
    {
        ProductId = productId;
        Description = description;
        PromotionalPrice = promotionalPrice;
        StartDate = startDate;
        EndDate = endDate;
        // DaysAndTimes = daysAndTimes;
    }
}
