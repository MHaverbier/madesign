using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace wl.body.readmodels
{
    public class RMMapper
    {
        public IEnumerable<dynamic> RmToTm(IEnumerable<ListRM> listRm)
        {
            foreach (var rm in listRm)
            {
                dynamic tm = new ExpandoObject();
                tm.Id = rm.ListId;
                tm.Name = rm.ListName;
                tm.NumberOfTasks = rm.ActiveTaskCount;
                yield return tm;
            }
        }
    }
}
