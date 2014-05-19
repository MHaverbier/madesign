using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using it.contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace it.DBProvider
{
    public class MongoDbProvider : IDBProvider
    {
        private readonly MongoDatabase issueDatabase;

        public MongoDbProvider() : this( "mongodb://localhost" ) { }

        public MongoDbProvider( string connectionString )
        {
            var client = new MongoClient( connectionString );
            var server = client.GetServer();
            issueDatabase = server.GetDatabase( "IssueTriage" );
        }

        public void AlleIssuesLoeschen()
        {
            var issueCollection = issueDatabase.GetCollection<Issue>("issues");
            issueCollection.RemoveAll();
        }

        public IEnumerable<Issue> AllelIssuesLesen()
        {
            var issueCollection = issueDatabase.GetCollection<Issue>( "issues" );
            return issueCollection.FindAll().ToArray();
        }

        public void IssueInDBSchreiben( Issue issue )
        {
            var issueCollection = issueDatabase.GetCollection<Issue>( "issues" );
            issueCollection.Insert( issue );
        }
    }
}
