using System;
using System.Windows;
using it.DBProvider;
using it.logonprovider;

namespace it.HLClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var dbProvider = new MongoDbProvider();
            var logonProvider = new WindowsLogonProvider();
            var hlIntegration = new HLIntegration(dbProvider, logonProvider);
#if DEBUG
            var ui = new HL();
            ui.Show();
            ui.SubmitButtonClickEvent += hlIntegration.Melden;
#endif
        }
    }
}
