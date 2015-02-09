using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wl.body.readmodels
{
    class ListRM
    {
        public ListRM(string listId, string listName)
        {
            ListId = listId;
            ListName = listName;
            TaskIds = new List<TaskRm>();
        }

        public struct TaskRm
        {
            public string TaskId { get; set; }
            public bool IsActive { get; set; }
        }

        public string ListId { get; private set; }

        public string ListName { get; private set; }

        public List<TaskRm> TaskIds { get; private set; }
    }
}
