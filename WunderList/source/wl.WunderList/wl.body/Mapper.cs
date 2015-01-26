using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using wl.body.datamodels;

namespace wl.body
{
    public class Mapper
    {
        public static IEnumerable<dynamic> MapLists2Vm(IEnumerable<ListDM> lists)
        {
            foreach (var listDm in lists)
            {
                dynamic listProjection = new ExpandoObject();
                listProjection.Id = listDm.Id;
                listProjection.Name = listDm.Name;
                listProjection.NumberOfTasks = listDm.Tasks.Count(t => t.ActivationState == ActivationStates.Active);
                yield return listProjection;
            }
        }

        public static IEnumerable<dynamic> MapTasks2Vm(IEnumerable<TaskDM> filteredTasks)
        {
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
    }
}
