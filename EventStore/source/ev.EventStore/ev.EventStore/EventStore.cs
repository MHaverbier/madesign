using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ev.EventStore
{
    public class EventStore
    {
        private int aktuelleSequenzNummer;

        public int Record(Event evt)
        {
            var neueSequenzNummer = aktuelleSequenzNummer + 1;
            
            var dateiName = string.Format("Event-{0:000000000000}.evt", neueSequenzNummer);

            using (var datei =  new StreamWriter(dateiName))
            {
                datei.WriteLine(neueSequenzNummer);
                datei.WriteLine(evt.ContextId);
                datei.WriteLine(evt.EventName);
                datei.Write(evt.Payload);
            }

            aktuelleSequenzNummer = neueSequenzNummer;
            return neueSequenzNummer;
        }

        public IEnumerable<Event> Replay()
        {
            var eventDateien = Directory.EnumerateFiles(".", "Event-*.evt").OrderBy(name => name);

            foreach (var dateiName in eventDateien) {
                using (var datei = new StreamReader(dateiName))
                {
                    var sequenzNummer = int.Parse(datei.ReadLine());
                    var contextId = int.Parse(datei.ReadLine());
                    var eventName = datei.ReadLine();
                    var payload = datei.ReadToEnd();

                    yield return new Event(sequenzNummer, contextId, eventName, payload);
                }
            }
           
            yield break;
        }
    }
}