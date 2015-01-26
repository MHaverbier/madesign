using System;
using eventstore;

namespace wl.body
{
    public class Repository
    {
        public string AddList(string listName)
        {
            var storeEvent = new Event {Name = "ListAdded", ContextId = Guid.NewGuid().ToString(), Payload = listName};
            
            var eventStore = new FileEventstore("./myStore");
            eventStore.Record(storeEvent);

            return storeEvent.ContextId;
        }
    }
}