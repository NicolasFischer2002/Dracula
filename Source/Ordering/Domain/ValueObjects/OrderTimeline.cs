using Ordering.Domain.Enums;

namespace Ordering.Domain.ValueObjects
{
    public sealed record OrderTimeline
    {
        public SortedDictionary<DateTime, ActionTimelineOfTheOrder> TimelineEvents { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public OrderTimeline(DateTime orderCreationMoment)
        {
            TimelineEvents = new SortedDictionary<DateTime, ActionTimelineOfTheOrder>();

            TimelineEvents.Add(orderCreationMoment, ActionTimelineOfTheOrder.OrderCreated);
            LastUpdated = orderCreationMoment;
        }

        public void AddEvent(DateTime eventTime, ActionTimelineOfTheOrder action)
        {
            TimelineEvents.Add(eventTime, action);
            LastUpdated = eventTime;
        }
    }
}