using System.Collections.Generic;

namespace it.contracts
{
    public interface IDBProvider
    {
        IEnumerable<Issue> AllelIssuesLesen();

        void IssueInDBSchreiben(Issue issue);
    }
}
