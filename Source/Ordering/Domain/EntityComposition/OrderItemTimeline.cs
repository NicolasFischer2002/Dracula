using Ordering.Domain.Enums;
using Ordering.Domain.Exceptions;
using SharedKernel.ValueObjects;
using System.Collections.ObjectModel;

public sealed class OrderItemTimeline
{
    private readonly SortedDictionary<DateTime, ActionTimelineOfTheOrderItem> _events;
    public IReadOnlyDictionary<DateTime, ActionTimelineOfTheOrderItem> Events =>
        new ReadOnlyDictionary<DateTime, ActionTimelineOfTheOrderItem>(_events);

    public DateTime LastUpdated { get; private set; }

    public OrderItemTimeline(UtcInstant orderCreationMoment)
    {
        OrderException.ThrowIfNull(orderCreationMoment, nameof(orderCreationMoment));

        var creationTime = orderCreationMoment.Value;
        _events = new SortedDictionary<DateTime, ActionTimelineOfTheOrderItem>
        {
            [creationTime] = ActionTimelineOfTheOrderItem.OrderItemCreated
        };
        LastUpdated = creationTime;
    }

    public void AddEvent(UtcInstant eventTime, ActionTimelineOfTheOrderItem action)
    {
        OrderException.ThrowIfNull(eventTime, nameof(eventTime));

        var eventTimestamp = eventTime.Value;
        FailIfEventTimestampAlreadyExists(eventTimestamp);

        _events.Add(eventTimestamp, action);
        LastUpdated = eventTimestamp;
    }

    private void FailIfEventTimestampAlreadyExists(DateTime eventTimestamp)
    {
        OrderException.ThrowIf(
            _events.ContainsKey(eventTimestamp),
            eventTimestamp.ToString(),
            "Já existe um evento registrado no mesmo timestamp."
        );
    }
}