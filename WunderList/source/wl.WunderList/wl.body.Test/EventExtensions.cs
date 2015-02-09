using eventstore;

namespace wl.body.Test
{
    public static class EventExtensions
    {
        public static long sequenceNumber = 0L;

        public static void Reset()
        {
            sequenceNumber = 0L;
        }

        public static Event Create(this string @eventName, string contextId, string payload = null)
        {
            sequenceNumber++;
            return new Event{ContextId = contextId, Name = @eventName, Sequencenumber = sequenceNumber, Payload = payload};

        }
    }
}