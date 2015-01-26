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
        private const string TASK_DEACTIVATED = "TaskDeactivated";

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
            IEnumerable<Event> allEvent = eventStore.Replay();

            var listDMs = new Dictionary<string, ListDM>();
            var taskDMs = new Dictionary<string, TaskDM>();

            foreach (Event @event in allEvent)
            {
                handleListEvents(listDMs, @event, taskDMs);
                handleTaskEvents(taskDMs, @event);
            }

            return listDMs.Values;
        }

        private void handleTaskEvents(Dictionary<string, TaskDM> taskDMs, Event @event)
        {
            switch (@event.Name)
            {
                case TASK_CREATED:
                {
                    var taskDm = new TaskDM {Id = @event.ContextId, Name = @event.Payload};
                    taskDMs.Add(@event.ContextId, taskDm);
                }
                    break;
                case TASK_DEACTIVATED:
                {
                    TaskDM taskDm = taskDMs[@event.ContextId];
                    taskDm.ActivationState = ActivationStates.Inactive;
                }
                    break;
            }
        }

        private void handleListEvents(Dictionary<string, ListDM> listDMs, Event @event,
            Dictionary<string, TaskDM> taskDMs)
        {
            switch (@event.Name)
            {
                case LIST_CREATED:
                    var listDm = new ListDM {Id = @event.ContextId, Name = @event.Payload};
                    listDMs.Add(@event.ContextId, listDm);
                    break;
                case TASK_ADDED_TO_LIST:
                    ListDM listToAddTaskTo = listDMs[@event.ContextId];
                    TaskDM taskToAdd = taskDMs[@event.Payload];
                    listToAddTaskTo.Tasks.Add(taskToAdd);
                    break;
            }
        }

        public string AddTask(string listId, string taskName)
        {
            string taskId = Guid.NewGuid().ToString();
            var createTaskEvent = new Event {Name = TASK_CREATED, ContextId = taskId, Payload = taskName};
            eventStore.Record(createTaskEvent);

            var addTaskToListEvent = new Event {Name = TASK_ADDED_TO_LIST, ContextId = listId, Payload = taskId};
            eventStore.Record(addTaskToListEvent);

            return taskId;
        }

        public IEnumerable<TaskDM> LoadTasks(string listId)
        {
            Event[] allEvents = eventStore.Replay().ToArray();

            IEnumerable<string> idsOfTasksInList = allEvents.Where(e => e.ContextId == listId)
                .Where(e => e.Name == TASK_ADDED_TO_LIST)
                .Select(e => e.Payload);

            IEnumerable<Event> taskRelevantEvents = allEvents.Where(e => idsOfTasksInList.Contains(e.ContextId));

            var taskDms = new Dictionary<string, TaskDM>();

            foreach (Event e in taskRelevantEvents)
            {
                handleTaskEvents(taskDms, e);
            }

            return taskDms.Values;
        }

        public void DeactivateTask(string taskId)
        {
            var e = new Event {ContextId = taskId, Name = TASK_DEACTIVATED};
            eventStore.Record(e);
        }
    }
}