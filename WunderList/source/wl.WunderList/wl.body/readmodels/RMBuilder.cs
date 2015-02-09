using System.Collections.Generic;
using System.Dynamic;
using eventstore;

namespace wl.body.readmodels
{
    public class RMBuilder
    {
        private const string LIST_CREATED = "ListCreated";
        private const string TASK_ADDED_TO_LIST = "TaskAddedToList";
        private const string TASK_ACTIVATED = "TaskActivated";
        private const string TASK_DEACTIVATED = "TaskDeactivated";

        public IEnumerable<dynamic> Build(IEnumerable<Event> events)
        {
            var listId2Result = new Dictionary<string, dynamic>();
            var taskId2ListId = new Dictionary<string, string>();
            foreach (var storeEvent in events)
            {
                switch (storeEvent.Name)
                {
                    case LIST_CREATED:
                        {
                            dynamic listProjection = new ExpandoObject();
                            listProjection.Id = storeEvent.ContextId;
                            listProjection.Name = storeEvent.Payload;
                            listProjection.NumberOfTasks = 0;
                            listId2Result.Add(listProjection.Id, listProjection);
                        }
                        break;
                    case TASK_ADDED_TO_LIST:
                    {
                        taskId2ListId.Add(storeEvent.Payload, storeEvent.ContextId);
                    }
                        break;
                    case TASK_ACTIVATED:
                    {
                        string listID = taskId2ListId[storeEvent.ContextId];
                        dynamic tm = listId2Result[listID];
                        tm.NumberOfTasks++;
                    }
                        break;
                    case TASK_DEACTIVATED:
                    {
                        string listID = taskId2ListId[storeEvent.ContextId];
                        dynamic tm = listId2Result[listID];
                        tm.NumberOfTasks--;
                    }
                        break;
                }
            }
            return listId2Result.Values;
        }

        public IEnumerable<dynamic> Build(Event storeEvent, IEnumerable<dynamic> tranferModels)
        {
            return null;
        }
    }
}
