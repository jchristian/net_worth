using System.Collections.Generic;
using System.Linq;
using core.events.handlers;
using MoreLinq;

namespace core.events
{
    public class EventBus
    {
        IList<object> subscribers;

        public EventBus(CreateLotsForBrokerageTransactions handler)
        {
            subscribers = new object[] { handler };
        }

        public void Subscribe<TEvent>(IHandle<TEvent> subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void Raise<TEvent>(TEvent @event)
        {
            subscribers.OfType<IHandle<TEvent>>().ForEach(x => x.Handle(@event));
        }
    }
}