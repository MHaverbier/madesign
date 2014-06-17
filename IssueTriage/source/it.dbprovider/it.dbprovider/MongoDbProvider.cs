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

        /// <summary>
        /// Creates database provider for 'mongodb://localhost'.
        /// </summary>
        public MongoDbProvider() : this( "mongodb://localhost" ) { }

        /// <summary>
        /// Creates the database provider using the given mongo database connection.
        /// </summary>
        /// <param name="connectionString">The non null connection string like 'mongodb://localhost'.</param>
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
