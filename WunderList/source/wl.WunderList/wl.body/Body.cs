using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using wl.body.datamodels;
using wl.body.readmodels;

namespace wl.body
{
    public class Body
    {
        private readonly Repository _repository;
        private readonly RMManager _readModelManager;

        public Body(Repository repository, RMManager readModelManager)
        {
            _repository = repository;
            _readModelManager = readModelManager;
        }


        public string AddList(string listName)
        {
            return _repository.AddList(listName);
        }

        public IEnumerable<dynamic> ShowLists()
        {
            return _readModelManager.Read();
        }

        public string AddTask(string listId, string taskName)
        {
            return _repository.AddTask(listId, taskName);
        }

        public IEnumerable<dynamic> ShowTasks(string listId, ActivationStates activityState)
        {
            var tasks = _repository.LoadTasks(listId);
            var filteredTasks = tasks.Where(t => t.ActivationState == activityState);

            return Mapper.MapTasks2Vm(filteredTasks);
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
