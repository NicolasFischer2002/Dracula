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
        _events = new SortedDictionary<DateTime, ActionTimelineOfTheOrderItem>
        {
            [orderCreationMoment.Value] = ActionTimelineOfTheOrderItem.OrderItemCreated
        };

        LastUpdated = orderCreationMoment.Value;
    }

    public void AddEvent(UtcInstant eventTime, ActionTimelineOfTheOrderItem action)
    {
        var when = eventTime.Value;

        if (_events.ContainsKey(when))
            throw new OrderException("Já existe um evento na mesma marca temporal.", when.ToString());

        _events.Add(when, action);
        LastUpdated = when;
    }
}