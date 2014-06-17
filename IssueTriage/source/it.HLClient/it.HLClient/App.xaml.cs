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
            var ui = new HL();
            
            ui.SubmitButtonClickEvent += hlIntegration.Melden;
            
            ui.Show();
        }
    }
}
