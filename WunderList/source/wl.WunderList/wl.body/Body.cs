using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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

            return Mapper.MapLists2Vm(lists);
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
