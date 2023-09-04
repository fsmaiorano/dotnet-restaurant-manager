using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Promotion;

public class PromotionDeletedEvent : BaseEvent
{
    public PromotionDeletedEvent(PromotionEntity promotion)
    {
        Promotion = promotion;
    }

    public PromotionEntity Promotion { get; set; }
}
