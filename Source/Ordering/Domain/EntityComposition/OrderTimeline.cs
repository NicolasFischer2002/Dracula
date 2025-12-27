using Ordering.Domain.Enums;
using Ordering.Domain.Exceptions;
using SharedKernel.ValueObjects;
using System.Collections.ObjectModel;

namespace Ordering.Domain.EntityComposition
{
    public sealed class OrderTimeline
    {
        private readonly SortedDictionary<DateTime, ActionTimelineOfTheOrder> _events;

        public IReadOnlyDictionary<DateTime, ActionTimelineOfTheOrder> Events =>
            new ReadOnlyDictionary<DateTime, ActionTimelineOfTheOrder>(_events);

        public DateTime LastUpdated { get; private set; }

        public OrderTimeline(UtcInstant orderCreationMoment)
        {
            _events = new SortedDictionary<DateTime, ActionTimelineOfTheOrder>
            {
                [orderCreationMoment.Value] = ActionTimelineOfTheOrder.OrderCreated
            };

            LastUpdated = orderCreationMoment.Value;
        }

        public void AddEvent(UtcInstant eventTime, ActionTimelineOfTheOrder action)
        {
            var moment = eventTime.Value;

            if (_events.ContainsKey(moment))
                throw new OrderException(
                    "Já existe um evento registrado para este instante.",
                    moment.ToString()
                );

            _events.Add(moment, action);
            LastUpdated = moment;
        }
    }
}