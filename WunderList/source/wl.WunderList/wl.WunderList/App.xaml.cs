using eventstore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using wl.body;
using wl.body.readmodels;

namespace wl.WunderList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var eventStore = new FileEventstore("./myStore");
            var repository = new Repository(eventStore);
            var rmProvider = new RMProvider("./myRmStore");
            var rmBuilder = new RMBuilder();
            var rmMapper = new RMMapper();
            var readModelManager = new RMManager(rmProvider, rmBuilder, rmMapper, eventStore);
            var body = new Body(repository, readModelManager);
            var headVM = new HeadViewModel(body);

            var head = new Head();
            head.DataContext = headVM;
            head.Show();

        }
    }
}
