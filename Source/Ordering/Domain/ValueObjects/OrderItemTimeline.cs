using Ordering.Domain.Enums;

namespace Ordering.Domain.ValueObjects
{
    public sealed record OrderItemTimeline
    {
        public SortedDictionary<DateTime, ActionTimelineOfTheOrderItem> TimelineEvents { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public OrderItemTimeline(DateTime orderCreationMoment)
        {
            TimelineEvents = new SortedDictionary<DateTime, ActionTimelineOfTheOrderItem>();

            TimelineEvents.Add(orderCreationMoment, ActionTimelineOfTheOrderItem.OrderItemCreated);
            LastUpdated = orderCreationMoment;
        }

        public void AddEvent(DateTime eventTime, ActionTimelineOfTheOrderItem action)
        {
            TimelineEvents.Add(eventTime, action);
            LastUpdated = eventTime;
        }
    }
}