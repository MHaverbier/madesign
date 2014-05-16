using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using it.contracts;

namespace it.logonprovider
{
    public class WindowsLogonProvider : ILogonProvider
    {
        public string WindowsLogonBestimmen()
        {
            return Environment.UserName;
        }
    }
}
