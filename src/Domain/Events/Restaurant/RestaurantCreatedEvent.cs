using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Restaurant;

public class RestaurantCreatedEvent : BaseEvent
{
    public RestaurantCreatedEvent(RestaurantEntity restaurant)
    {
        Restaurant = restaurant;
    }

    public RestaurantEntity Restaurant { get; set; }
}
