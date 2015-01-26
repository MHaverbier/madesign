using System;
using System.Collections.Generic;
using System.Linq;
using eventstore;
using wl.body.datamodels;

namespace wl.body
{
    public class Repository
    {
        private const string LIST_CREATED = "ListCreated";
        private const string TASK_CREATED = "TaskCreated";
        private const string TASK_ADDED_TO_LIST = "TaskAddedToList";

        private readonly FileEventstore eventStore;

        public Repository(FileEventstore eventStore)
        {
            this.eventStore = eventStore;
        }

        public string AddList(string listName)
        {
            var storeEvent = new Event {Name = LIST_CREATED, ContextId = Guid.NewGuid().ToString(), Payload = listName};
            
            eventStore.Record(storeEvent);
            return storeEvent.ContextId;
        }

        public IEnumerable<ListDM> LoadLists()
        {
            var allEvent = eventStore.Replay();

            var listDMs = new Dictionary<string, ListDM>();
            var taskDMs = new Dictionary<string, TaskDM>();

            foreach (var @event in allEvent)
            {
                switch (@event.Name)
                {
                    case LIST_CREATED:
                        var listDm = new ListDM { Id = @event.ContextId, Name = @event.Payload};
                        listDMs.Add(@event.ContextId, listDm);
                        break;
                    case TASK_CREATED:
                        var taskDm = new TaskDM { Id = @event.ContextId, Name = @event.Payload };
                        taskDMs.Add(@event.ContextId, taskDm);
                        break;
                    case TASK_ADDED_TO_LIST:
                        var listToAddTaskTo = listDMs[@event.ContextId];
                        var taskToAdd = taskDMs[@event.Payload];
                        listToAddTaskTo.Tasks.Add(taskToAdd);
                        break;
                }
            }

            return listDMs.Values;
        }

        public string AddTask(string listId, string taskName)
        {
            var taskId = Guid.NewGuid().ToString();
            var createTaskEvent = new Event { Name = TASK_CREATED, ContextId = taskId, Payload = taskName };
            eventStore.Record(createTaskEvent);

            var addTaskToListEvent = new Event { Name = TASK_ADDED_TO_LIST, ContextId = listId, Payload = taskId };
            eventStore.Record(addTaskToListEvent);

            return taskId;
        }
    }
}