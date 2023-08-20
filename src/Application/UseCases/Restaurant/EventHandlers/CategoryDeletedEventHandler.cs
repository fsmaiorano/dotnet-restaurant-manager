using Domain.Events.Restaurant;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Restaurant.EventHandlers;

public class RestaurantDeletedEventHandler : INotificationHandler<RestaurantDeletedEvent>
{
    private readonly ILogger<RestaurantDeletedEventHandler> _logger;

    public RestaurantDeletedEventHandler(ILogger<RestaurantDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(RestaurantDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
