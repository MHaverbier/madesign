using it.contracts;

namespace it.HLClient
{
    public class Integration
    {
        private readonly IDBProvider _dbProvider;
        private readonly ILogonProvider _logonProvider;

        public Integration(IDBProvider dbProvider, ILogonProvider logonProvider)
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
