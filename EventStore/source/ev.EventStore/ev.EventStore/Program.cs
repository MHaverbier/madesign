using System;

namespace ev.EventStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventStore = new EventStore();

            Console.WriteLine("-- 1. Event --");
            var myEvent = new Event(1, "Event 1", "Payload 1");
            eventStore.Record(myEvent);
            foreach (var @event in eventStore.Replay())
            {
                Console.WriteLine("ContextID:{0} - EventName:{1} - Payload:{2}", @event.ContextId + @event.EventName, @event.Payload);
            }

            Console.WriteLine("-- 2. Event --");
            var myEvent2 = new Event(2, "Event 2", "Payload 2");
            eventStore.Record(myEvent2);
            foreach (var @event in eventStore.Replay())
            {
                Console.WriteLine("ContextID:{0} - EventName:{1} - Payload:{2}", @event.ContextId + @event.EventName, @event.Payload);
            }

            Console.ReadLine();
        }
    }
}
