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
        private const string TASK_MOVED = "TaskMoved";
        private const string TASK_IMPORTANCE_TOGGLED = "TaskImportanceToggled";

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

            foreach (var @event in allEvent)
            {
                HandleListEvents(listDMs, @event, taskDMs);
                HandleTaskEvents(taskDMs, @event);
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
            var allEvents = eventStore.Replay().ToArray();

            var idsOfTasksInList = allEvents.Where(e => e.ContextId == listId)
                .Where(e => e.Name == TASK_ADDED_TO_LIST)
                .Select(e => e.Payload);

            var taskRelevantEvents = allEvents.Where(e => idsOfTasksInList.Contains(e.ContextId));

            var taskDms = new List<TaskDM>();

            foreach (var e in taskRelevantEvents)
            {
                HandleTaskEvents(taskDms, e);
            }

            return taskDms;
        }

        public void DeactivateTask(string taskId)
        {
            var e = new Event { ContextId = taskId, Name = TASK_DEACTIVATED };
            eventStore.Record(e);
        }

        private void HandleTaskEvents(List<TaskDM> taskDMs, Event @event)
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
                        var taskDm = taskDMs.First(t => t.Id == @event.ContextId);
                        taskDm.ActivationState = ActivationStates.Inactive;
                    }
                    break;
                case TASK_MOVED:
                    {
                        var sourceTaskDm = taskDMs.First(t => t.Id == @event.ContextId);
                        taskDMs.Remove(sourceTaskDm);
                        var destinationTaskDm = taskDMs.First(t => t.Id == @event.Payload);
                        var index = taskDMs.IndexOf(destinationTaskDm);
                        taskDMs.Insert(index, sourceTaskDm);
                    }
                    break;
                case TASK_IMPORTANCE_TOGGLED:
                    {
                        var taskDm = taskDMs.First(t => t.Id == @event.ContextId);
                        taskDm.IsImportant = !taskDm.IsImportant;
                    }
                    break;
            }
        }

        private void HandleListEvents(ICollection<ListDM> listDMs, Event @event, IEnumerable<TaskDM> taskDMs)
        {
            switch (@event.Name)
            {
                case LIST_CREATED:
                    var listDm = new ListDM { Id = @event.ContextId, Name = @event.Payload };
                    listDMs.Add(listDm);
                    break;
                case TASK_ADDED_TO_LIST:
                    var listToAddTaskTo = listDMs.First(l => l.Id == @event.ContextId);
                    var taskToAdd = taskDMs.First(t => t.Id == @event.Payload);
                    listToAddTaskTo.Tasks.Add(taskToAdd);
                    break;
            }
        }

        public void MoveTask(string sourceId, string destinationId)
        {
            var e = new Event { ContextId = sourceId, Name = TASK_MOVED, Payload = destinationId };
            eventStore.Record(e);
        }

        public void ToggleImportance(string taskId)
        {
            var e = new Event { ContextId = taskId, Name = TASK_IMPORTANCE_TOGGLED };
            eventStore.Record(e);
        }
    }
}