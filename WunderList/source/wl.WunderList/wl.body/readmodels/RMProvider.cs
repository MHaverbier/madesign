using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wl.body.readmodels
{
    public class RMProvider
    {


        public bool IsInitialized()
        {
            return false;
        }

        public void Persist(IEnumerable<dynamic> tms)
        {

        }

        public IEnumerable<dynamic> Depersist()
        {
            return null;
        }

    }
}
