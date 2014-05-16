using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using it.contracts;

namespace it.logonprovider.tests
{
    [TestFixture]
    public class test_WindowsLogonProvider
    {
        [Test, Explicit]
        public void Windows_Logon_bestimmen()
        {
            ILogonProvider sut = new WindowsLogonProvider();
            Console.WriteLine(sut.WindowsLogonBestimmen());
        }
    }
}
