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
            OrderException.ThrowIfNull(
                orderCreationMoment, 
                "O momento de criação da linha do tempo do pedido não pode ser nulo"
            );

            var creationTime = orderCreationMoment.Value;
            _events = new SortedDictionary<DateTime, ActionTimelineOfTheOrder>
            {
                [creationTime] = ActionTimelineOfTheOrder.OrderCreated
            };
            LastUpdated = creationTime;
        }

        public void AddEvent(UtcInstant eventTime, ActionTimelineOfTheOrder action)
        {
            OrderException.ThrowIfNull(
                eventTime, 
                "Não é possível adicionar um evento nulo na linha do tempo do pedido."
            );

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
                "Já existe um evento registrado para este timestamp."
            );
        }
    }
}