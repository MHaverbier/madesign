using System.Collections.Generic;

namespace wl.body.datamodels
{
    public class ListDM
    {
        public string Id;
        public string Name;
        public List<TaskDM> Tasks = new List<TaskDM>();
    }
}