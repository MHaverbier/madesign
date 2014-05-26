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
            var mainWindow = new MainWindow();
            var mapper = new IssueMapper();
            var dbPoriver = new DBProvider.MongoDbProvider();

            // Bind
            mainWindow.IssueInfos = mapper.IssueInfosErzeugen(dbPoriver.AllelIssuesLesen());

            // Start
            mainWindow.Show();
        }
    }

    public class IssueMapper
    {
        public IEnumerable<IssueInfo> IssueInfosErzeugen(IEnumerable<Issue> issue)
        {
            return issue.Select(currentIssue => new IssueInfo
            {
                Meldername = currentIssue.MelderName, Beschreibung = currentIssue.Beschreibung
            }).ToList();
        }
    }
}
