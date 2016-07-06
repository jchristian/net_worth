using core.events;
using core.events.handlers;
using Xunit;

namespace core.tests.events
{
    public class EventBusTests
    {
        [Fact]
        public void should_execute_the_handler_when_an_event_is_raised()
        {
            var handler = new Moq.Mock<CreateLotsForBrokerageTransactions>();
            var @event = new BrokerageTransactionsPersisted(null);

            new EventBus(handler.Object).Raise(@event);

            handler.Verify(x => x.Handle(@event));
        }
    }
}