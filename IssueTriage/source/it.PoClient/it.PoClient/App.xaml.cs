using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace it.PoClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            PoClientIntegration integration = new PoClientIntegration();
            UI ui = new UI();
            IssueMapper mapper = new IssueMapper();
        }
    }

    public class UI
    {
    }

    public class PoClientIntegration
    {
    }

    public class IssueMapper
    {
    }
}
