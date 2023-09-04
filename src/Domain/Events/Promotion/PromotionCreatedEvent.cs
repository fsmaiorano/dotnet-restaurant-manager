using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Promotion;

public class PromotionCreatedEvent : BaseEvent
{
    public PromotionCreatedEvent(PromotionEntity promotion)
    {
        Promotion = promotion;
    }

    public PromotionEntity Promotion { get; set; }
}
