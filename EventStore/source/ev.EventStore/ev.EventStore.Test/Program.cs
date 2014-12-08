using System;

namespace ev.EventStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventStore = new EventStore();

            Console.WriteLine("-- 1. Event --");
            var myEvent = new Event(Guid.NewGuid().ToString(), "Event 1", "Payload 1" + DateTime.Now);
            eventStore.Record(myEvent);
            PrintEvents(eventStore);

            Console.WriteLine("-- 2. Event --");
            var myEvent2 = new Event("2", "Event 2", "Payload 2" + DateTime.Now);
            eventStore.Record(myEvent2);
            PrintEvents(eventStore);

            Console.ReadLine();
        }

        private static void PrintEvents(EventStore eventStore)
        {
            foreach (var @event in eventStore.Replay())
            {
                Console.WriteLine("SequenceNumber:{0} - ContextID:{1} - EventName:{2} - Payload:{3}", @event.SequenceNumber, @event.ContextId, @event.EventName, @event.Payload);
            }
        }
    }
}
