﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using it.contracts;

namespace it.DBProvider
{
    public class MongoDbProbider : IDBProvider
    {
        public IEnumerable<Issue> AllelIssuesLesen()
        {
            yield return new Issue() {Beschreibung = "Test", Id = Guid.NewGuid(), MelderName = "John Doe"};
        }

        public void IssueInDBSchreiben(Issue issue)
        {
            
        }
    }
}
