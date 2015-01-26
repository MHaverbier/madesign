using System;
using System.Collections.Generic;
using System.Linq;
using eventstore;
using wl.body.datamodels;

namespace wl.body
{
    public class Repository
    {
        private const string ListAddedEventName = "ListAdded";
        private readonly FileEventstore eventStore;

        public Repository(FileEventstore eventStore)
        {
            this.eventStore = eventStore;
        }

        public string AddList(string listName)
        {
            var storeEvent = new Event {Name = ListAddedEventName, ContextId = Guid.NewGuid().ToString(), Payload = listName};
            
            eventStore.Record(storeEvent);

            return storeEvent.ContextId;
        }

        public IEnumerable<ListDM> LoadLists()
        {
            var allEvent = eventStore.Replay();
            var listDMs = allEvent.Where(myEvent => myEvent.Name.Equals(ListAddedEventName)).Select( myEvent =>
                new ListDM { Id = myEvent.ContextId, Name = myEvent.Payload, NumberOfTasks = 0 });
            
            return listDMs;
        }
    }
}