namespace core.events.handlers
{
    public interface IHandle<TEvent>
    {
        void Handle(TEvent @event);
    }
}