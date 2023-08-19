using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Restaurant;

public class RestaurantDeletedEvent : BaseEvent
{
    public RestaurantDeletedEvent(RestaurantEntity restaurant)
    {
        Restaurant = restaurant;
    }

    public RestaurantEntity Restaurant { get; set; }
}
