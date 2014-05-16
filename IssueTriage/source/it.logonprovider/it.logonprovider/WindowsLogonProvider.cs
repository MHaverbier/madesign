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
            var username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            var i = username.IndexOf("\\");
            if (i >= 0) username = username.Substring(i + 1);
            return username;
        }
    }
}
