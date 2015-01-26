using System.Collections.Generic;
using System.Dynamic;
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
            // get all lists

            // get numberoftask per list item

            var lists = _repository.LoadLists();

            foreach (var listDm in lists)
            {
                dynamic listProjection = new ExpandoObject();
                listProjection.Id = listDm.Id;
                listProjection.Name = listDm.Name;
                listProjection.NumberOfTasks = listDm.NumberOfTasks;
                yield return listProjection;
            }
            
        }

        public string AddTask(string listId, string taskName)
        {
            return _repository.AddTask(listId, taskName);
        }
    }
}
