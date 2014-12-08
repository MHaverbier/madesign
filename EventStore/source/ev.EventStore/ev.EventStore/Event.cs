namespace ev.EventStore
{
    public class Event
    {
        public Event(int contextId, string eventName, string payload)
        {
            ContextId = contextId;
            EventName = eventName;
            Payload = payload;
        }

        public int ContextId { get; private set; }

        public string EventName { get; private set; }

        public string Payload { get; private set; }
    }
}