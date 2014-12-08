using System;
namespace ev.EventStore
{
    public class Event
    {
        internal Event(int sequenceNumber, string contextId, string eventName, string payload)
        {
            SequenceNumber = sequenceNumber;
            ContextId = contextId;
            EventName = eventName;
            Payload = payload;
        }

        public Event(string contextId, string eventName, string payload)
            : this(0, contextId, eventName, payload)
        {
        }

        public int SequenceNumber { get; private set; }

        public string ContextId { get; private set; }

        public string EventName { get; private set; }

        public string Payload { get; private set; }
    }
}