using System.Collections.Generic;
using System.Dynamic;

namespace wl.body.readmodels
{
    public class RMMapper
    {
        public IEnumerable<dynamic> RmToTm(List<ListRM> listRm)
        {
            foreach (var rm in listRm)
            {
                dynamic tm = new ExpandoObject();
                tm.Id = rm.ListId;
                tm.Name = rm.ListName;
                tm.NumberOfTasks = rm.TaskIds.Count;
                yield return tm;
            }
        }
    }
}
