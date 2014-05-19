using it.contracts;

namespace it.HLClient
{
    public class HLIntegration
    {
        private readonly IDBProvider _dbProvider;
        private readonly ILogonProvider _logonProvider;

        public HLIntegration(IDBProvider dbProvider, ILogonProvider logonProvider)
        {
            _dbProvider = dbProvider;
            _logonProvider = logonProvider;
        }

        public void Melden(string beschreibung)
        {
            var userName = _logonProvider.WindowsLogonBestimmen();
            var issue = new Issue {Beschreibung = beschreibung, MelderName = userName};
            _dbProvider.IssueInDBSchreiben(issue);
        }
    }
}
