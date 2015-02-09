using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using eventstore;

namespace wl.body.readmodels
{
    public class RMBuilder
    {
        private const string LIST_CREATED = "ListCreated";
        private const string TASK_ADDED_TO_LIST = "TaskAddedToList";
        private const string TASK_ACTIVATED = "TaskActivated";
        private const string TASK_DEACTIVATED = "TaskDeactivated";

        public IEnumerable<ListRM> Build(IEnumerable<Event> events)
        {
            var list = new List<ListRM>();
            foreach (var @event in events)
            {
                Build(@event, list);
            }
            return list;
        }

        public IEnumerable<ListRM> Build(Event storeEvent, IEnumerable<ListRM> transferModels)
        {
            var result = new List<ListRM>(transferModels);
            Build(storeEvent, result);
            return result;
        }

        private void Build(Event storeEvent, List<ListRM> transferModels)
        {
            switch (storeEvent.Name)
            {
                case LIST_CREATED:
                    {
                        var listProjection = new ListRM(storeEvent.ContextId, storeEvent.Payload);
                        transferModels.Add(listProjection);
                    }
                    break;
                case TASK_ADDED_TO_LIST:
                    {
                        var listID = storeEvent.ContextId;
                        var tmodel = transferModels.First(tm => tm.ListId == listID);
                        tmodel.TaskIds.Add(new ListRM.TaskRm { TaskId = storeEvent.Payload, IsActive = true });
                    }
                    break;
                case TASK_ACTIVATED:
                    {
                        var taskID = storeEvent.ContextId;
                        transferModels
                            .SelectMany(l => l.TaskIds)
                            .First(t => t.TaskId == taskID)
                            .IsActive = true;
                      
                    }
                    break;
                case TASK_DEACTIVATED:
                    {
                        var taskID = storeEvent.ContextId;
                        transferModels
                            .SelectMany(l => l.TaskIds)
                            .First(t => t.TaskId == taskID)
                            .IsActive = false;
                    }
                    break;
            }
        }
    }
}
