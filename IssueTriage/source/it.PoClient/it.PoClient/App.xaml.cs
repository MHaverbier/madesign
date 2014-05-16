using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using it.contracts;

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
            // Create
            UI ui = new UI();
            IssueMapper mapper = new IssueMapper();
            PoClientIntegration integration = new PoClientIntegration(mapper);

            // Bind
            integration.Start += ui.Start;
        }
    }

    public class UI
    {
        public void Start()
        {
            throw new NotImplementedException();
        }
    }

    public class PoClientIntegration
    {
        public event Action Start;
    }

    public class IssueMapper
    {
        public IEnumerable<IssueInfo> IssueInfosErzeugen(IEnumerable<Issue> issue)
        {
            
        }
    }
}
