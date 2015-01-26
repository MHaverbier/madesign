using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using eventstore;
using wl.body.datamodels;

namespace wl.body
{
    public class Body
    {
        private readonly Repository _repository;

        public Body(Repository repository)
        {
            _repository = repository;
        }


        public string AddList(string listName)
        {
            return _repository.AddList(listName);
        }

        public IEnumerable<dynamic> ShowLists()
        {
            var lists = _repository.LoadLists();

            foreach (var listDm in lists)
            {
                dynamic listProjection = new ExpandoObject();
                listProjection.Id = listDm.Id;
                listProjection.Name = listDm.Name;
                listProjection.NumberOfTasks = listDm.Tasks.Count(t => t.ActivationState == ActivationStates.Active);
                yield return listProjection;
            }
            
        }

        public string AddTask(string listId, string taskName)
        {
            return _repository.AddTask(listId, taskName);
        }

        public IEnumerable<dynamic> ShowTasks(string listId, ActivationStates activityState)
        {
            var tasks = _repository.LoadTasks(listId);
            var filteredTasks = tasks.Where(t => t.ActivationState == activityState);

            foreach (var task in filteredTasks)
            {
                dynamic taskProjection = new ExpandoObject();
                taskProjection.Id = task.Id;
                taskProjection.Name = task.Name;
                taskProjection.IsActive = task.ActivationState;
                taskProjection.IsImportant = task.IsImportant;
                yield return taskProjection;
            }
        }

        public void DeactivateTask(string taskId)
        {
            _repository.DeactivateTask(taskId);
        }

        public void MoveTask(string sourceId, string destinationId)
        {
            _repository.MoveTask(sourceId, destinationId);
        }

        public void ToggleImportance(string taskId)
        {
            _repository.ToggleImportance(taskId);
        }

        public void ActivateTask(string taskId)
        {
            _repository.ActivateTask(taskId);
        }
    }
}
