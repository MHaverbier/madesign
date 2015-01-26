using System;
using System.Collections.Generic;
using System.Linq;
using eventstore;
using wl.body.datamodels;

namespace wl.body
{
    public class Repository
    {
        private const string ListCreatedEventName = "ListCreated";
        private const string TaskCreatedEventName = "TaskCreated";
        private const string TaskAddedToListEventName = "TaskAddedToList";

        private readonly FileEventstore eventStore;

        public Repository(FileEventstore eventStore)
        {
            this.eventStore = eventStore;
        }

        public string AddList(string listName)
        {
            var storeEvent = new Event {Name = ListCreatedEventName, ContextId = Guid.NewGuid().ToString(), Payload = listName};
            
            eventStore.Record(storeEvent);
            return storeEvent.ContextId;
        }

        public IEnumerable<ListDM> LoadLists()
        {
            var allEvent = eventStore.Replay();
            var listDMs = allEvent.Where(myEvent => myEvent.Name.Equals(ListCreatedEventName)).Select( myEvent =>
                new ListDM { Id = myEvent.ContextId, Name = myEvent.Payload, NumberOfTasks = 0 });
            
            return listDMs;
        }

        public string AddTask(string listId, string taskName)
        {
            var taskId = Guid.NewGuid().ToString();
            var createTaskEvent = new Event { Name = TaskCreatedEventName, ContextId = taskId, Payload = taskName };
            eventStore.Record(createTaskEvent);

            var addTaskToListEvent = new Event { Name = TaskAddedToListEventName, ContextId = listId, Payload = taskId };
            eventStore.Record(addTaskToListEvent);

            return taskId;
        }
    }
}