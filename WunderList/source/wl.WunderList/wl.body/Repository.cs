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
            var storeEvent = new Event { Name = LIST_CREATED, ContextId = Guid.NewGuid().ToString(), Payload = listName };

            eventStore.Record(storeEvent);
            return storeEvent.ContextId;
        }

        public IEnumerable<ListDM> LoadLists()
        {
            IEnumerable<Event> allEvent = eventStore.Replay();

            var listDMs = new List<ListDM>();
            var taskDMs = new List<TaskDM>();

            foreach (Event @event in allEvent)
            {
                handleListEvents(listDMs, @event, taskDMs);
                handleTaskEvents(taskDMs, @event);
            }

            return listDMs;
        }

        public string AddTask(string listId, string taskName)
        {
            string taskId = Guid.NewGuid().ToString();
            var createTaskEvent = new Event { Name = TASK_CREATED, ContextId = taskId, Payload = taskName };
            eventStore.Record(createTaskEvent);

            var addTaskToListEvent = new Event { Name = TASK_ADDED_TO_LIST, ContextId = listId, Payload = taskId };
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

            var taskDms = new List<TaskDM>();

            foreach (Event e in taskRelevantEvents)
            {
                handleTaskEvents(taskDms, e);
            }

            return taskDms;
        }

        public void DeactivateTask(string taskId)
        {
            var e = new Event { ContextId = taskId, Name = TASK_DEACTIVATED };
            eventStore.Record(e);
        }

        private void handleTaskEvents(List<TaskDM> taskDMs, Event @event)
        {
            switch (@event.Name)
            {
                case TASK_CREATED:
                    {
                        var taskDm = new TaskDM { Id = @event.ContextId, Name = @event.Payload };
                        taskDMs.Add(taskDm);
                    }
                    break;
                case TASK_DEACTIVATED:
                    {
                        TaskDM taskDm = taskDMs.First(t => t.Id == @event.ContextId);
                        taskDm.ActivationState = ActivationStates.Inactive;
                    }
                    break;
            }
        }

        private void handleListEvents(List<ListDM> listDMs, Event @event, List<TaskDM> taskDMs)
        {
            switch (@event.Name)
            {
                case LIST_CREATED:
                    var listDm = new ListDM { Id = @event.ContextId, Name = @event.Payload };
                    listDMs.Add(listDm);
                    break;
                case TASK_ADDED_TO_LIST:
                    ListDM listToAddTaskTo = listDMs.First(l => l.Id == @event.ContextId);
                    TaskDM taskToAdd = taskDMs.First(t => t.Id == @event.Payload);
                    listToAddTaskTo.Tasks.Add(taskToAdd);
                    break;
            }
        }
    }
}