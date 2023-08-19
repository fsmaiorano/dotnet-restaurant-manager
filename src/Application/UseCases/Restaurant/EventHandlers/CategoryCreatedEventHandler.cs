using Domain.Events.Restaurant;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Restaurant.EventHandlers;

public class RestaurantCreatedEventHandler : INotificationHandler<RestaurantCreatedEvent>
{
    private readonly ILogger<RestaurantCreatedEventHandler> _logger;

    public RestaurantCreatedEventHandler(ILogger<RestaurantCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(RestaurantCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Blog: {DomainEvent}", notification.GetType().Name);
        _logger.LogDebug("Blog: {@DomainEvent}", notification);

        return Task.CompletedTask;
    }
}
