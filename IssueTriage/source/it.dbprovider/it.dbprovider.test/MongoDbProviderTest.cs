using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using it.contracts;
using it.DBProvider;
using NUnit.Framework;

namespace it.dbprovider.test
{
    [TestFixture]
    class MongoDbProviderTest
    {
        [Test]
        public void LesenUndSchreiben()
        {
            // Der Test sollte umgeschrieben werden; ich teste hier nur, dass ich etwas reinschreiben und wieder rauslesen kann
            // Da ich aber die Implementierung kenne, kann ich hier auch einen Whitebox Test machen; d.h. separierte MongoDB benutzen
            
            // Frage an Ralf: Wie testen wir solchen externen System eigentlich sauber

            var provider = new MongoDbProvider();

            provider.AlleIssuesLoeschen();

            var issue = new Issue() { Beschreibung = "Test", MelderName = "John Doe" };
            Assert.That( issue.Id, Is.EqualTo( Guid.Empty ) );

            provider.IssueInDBSchreiben( issue );

            Assert.That( issue.Id, Is.Not.EqualTo( Guid.Empty ) );

            var issues = provider.AllelIssuesLesen();

            Assert.That( issues, Is.Not.Empty );
            Assert.That( issues.First(), Is.Not.SameAs( issue ) );
            Assert.That( issues.First().Id, Is.EqualTo( issue.Id ) );

        }
    }
}
